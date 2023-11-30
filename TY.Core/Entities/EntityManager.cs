using TY.Collections;
using TY.Components;
using TY.Memory;
using TY.Unmanaged;
using ComponentType = TY.Unmanaged.ComponentType;

namespace TY.Entities;

public unsafe partial struct EntityManager
{
    public EntityDataAccess* entityDataAccess;

    public Entity CreateEntity()
    {
        return entityDataAccess->CreateEntity();
    }

    public Entity CreateEntity(params Type[] types)
    {
        throw new NotImplementedException();
    }

    public bool AddComponent<T>(Entity entity) where T : struct, IComponentData
    {
        throw new NotImplementedException();
    }

    public bool AddComponent<T>(Entity entity, T componentData) where T : struct, IComponentData
    {
        throw new NotImplementedException();
    }
}

/// <summary>
/// 实体数据访问
/// </summary>
public unsafe struct EntityDataAccess
{
    public UnsafeList<Archetype> archetypes;

    Archetype* _emptyArchetype;

    private Archetype* EmptyArchetype =>
        _emptyArchetype != null ? _emptyArchetype : _emptyArchetype = GetOrCreateArchetype(default, 0);

    // public UnsafePtrList<Chunk> chunks;

    /// <summary>
    /// chunk 信息数组，下标为entity的index
    /// </summary>
    public EntityChunkInfo* chunkInfo;

    public Entity CreateEntity()
    {
        throw new NotImplementedException();
    }

    public Archetype* GetOrCreateArchetype(ComponentType* types, int count)
    {
        // count + 1 表示默认增加一个Entity的位置
        var sortedComponents = stackalloc ComponentTypeInArchetype[count + 1];
        SortComponents(types, count, sortedComponents);

        foreach (var archetype in archetypes)
        {
            if (archetype.typesCount == count && Unsafe.MemoryCompare(archetype.types, types, count) == 0)
            {
                // todo return archetype;
            }
        }

        // todo  create archetype

        return null;
    }


    private void SortComponents(ComponentType* types, int count, ComponentTypeInArchetype* sorted)
    {
    }
}