using System.Runtime.InteropServices;
using TY.Collections;
using TY.Components;
using TY.Memory;
using TY.Unmanaged;

namespace TY.Entities;

public unsafe partial struct EntityManager
{
    public EntityDataAccess* entityDataAccess;

    public Entity CreateEntity()
    {
        return entityDataAccess->CreateEntity();
    }

    public Entity CreateEntity(params ComponentType[] types)
    {
        return entityDataAccess->CreateEntity(types);
    }

    public bool AddComponent<T>(Entity entity) where T : struct, IComponentData
    {
        throw new NotImplementedException();
    }

    public bool AddComponent<T>(Entity entity, T componentData) where T : struct, IComponentData
    {
        throw new NotImplementedException();
    }

    public EntityQuery Query(ComponentType* types, int count)
    {
        return entityDataAccess->Query(types, count);
    }
}

/// <summary>
/// 实体数据访问
/// </summary>
public unsafe struct EntityDataAccess
{
    public UnsafePtrList<Archetype> archetypes;

    Archetype* _emptyArchetype;

    private Archetype* EmptyArchetype =>
        _emptyArchetype != null ? _emptyArchetype : _emptyArchetype = GetOrCreateArchetype(default, 0);

    // public UnsafePtrList<Chunk> chunks;

    /// <summary>
    /// chunk 信息数组，下标为entity的index
    /// </summary>
    public UnsafeList<EntityInChunk> chunkInfo;


    public EntityQuery Query(ComponentType* types, int count)
    {
        //TODO 使用帧内存避免手动释放
        var matchingTypes = MemoryUtility.AllocZeroed<UnsafePtrList<Archetype>>();

        foreach (var archetype in archetypes)
        {
            {
                if (archetype->IsContainAllTypes(types, count))
                {
                    matchingTypes->Add(archetype);
                }
            }
        }

        return new EntityQuery() { matchingTypes = matchingTypes };
    }

    public Archetype* GetOrCreateArchetype(ComponentType* types, int count)
    {
        // count + 1 表示默认增加一个Entity的位置
        var sortedComponents = stackalloc ComponentTypeInArchetype[count + 1];
        SortComponents(types, count, sortedComponents);

        foreach (var archetype in archetypes)
        {
            if (archetype->typesCount == count && MemoryUtility.MemoryCompare(archetype->types, types, count) == 0)
            {
                return archetype;
            }
        }

        return CreateArchetype(sortedComponents, count + 1);
    }

    public Entity CreateEntity()
    {
        return CreateEntity(GetOrCreateArchetype(default, 0));
    }

    public Entity CreateEntity(ComponentType[] componentTypes)
    {
        fixed (ComponentType* types = componentTypes)
        {
            return CreateEntity(GetOrCreateArchetype(types, componentTypes.Length));
        }
    }

    public Entity CreateEntity(Archetype* archetype)
    {
        //获取有空格位置的Chunk
        var chunk = archetype->GetExistingChunkWithEmptySlots();

        //在chunk内开辟空间
        Entity entity;
        EntityInChunk entityInChunk;

        chunk->NextEntity(&entity, &entityInChunk, chunkInfo.Length);
        chunkInfo.Add(entityInChunk);

        return entity;
    }


    private Archetype* CreateArchetype(ComponentTypeInArchetype* sortedComponents, int count)
    {
        var byteSize = sizeof(ComponentTypeInArchetype) * count;
        var allocSortedComponents = (ComponentTypeInArchetype*)MemoryUtility.AllocZeroed((uint)byteSize);

        MemoryUtility.Copy(sortedComponents, allocSortedComponents, (uint)byteSize);

        var archetype = (Archetype*)MemoryUtility.AllocZeroed((uint)sizeof(Archetype));

        var typesSizes = (int*)MemoryUtility.AllocZeroed(sizeof(int));
        var typesOffsets = (int*)MemoryUtility.AllocZeroed(sizeof(int));

        archetype->types = allocSortedComponents;
        archetype->typesCount = count;
        archetype->chunkData.Add(SharedInstance<ChunkPool>.Instance.GetChunk());

        //类型大小存储
        archetype->typesSizes = typesSizes;
        for (var i = 0; i < count; i++)
        {
            var type = SharedInstance<TypeManager>.Instance.GetType(sortedComponents[i].TypeIndex);
            archetype->typesSizes[i] = Marshal.SizeOf(type);
        }

        //实体容纳个数
        archetype->capacityInChunk = Chunk.CalculateChunkCapacity(archetype->typesSizes, count);

        //类型偏移
        archetype->typesOffsets = typesOffsets;
        var usedBytes = 0;
        for (var c = 0; c < count; c++)
        {
            archetype->typesOffsets[c] = usedBytes;
            usedBytes += archetype->typesSizes[c] * archetype->capacityInChunk;
        }

        archetypes.Add(archetype);
        return archetype;
    }


    private void SortComponents(ComponentType* types, int count, ComponentTypeInArchetype* sorted)
    {
        sorted[0] = new ComponentTypeInArchetype
            { TypeIndex = SharedInstance<TypeManager>.Instance.TypeIndex(typeof(Entity)) };

        new Span<ComponentType>(types, count).Sort();

        MemoryUtility.Copy(types, sorted + 1, sizeof(ComponentType) * count);
    }
}