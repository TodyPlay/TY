using System.Runtime.CompilerServices;
using TY.Collections;
using TY.Components;

namespace TY.Unmanaged;

/// <summary>
/// 实体原型，记录实体的组件类型信息
/// </summary>
public unsafe struct Archetype
{
    /// <summary>
    /// 类型
    /// </summary>
    public ComponentTypeInArchetype* types;

    public int typesCount;

    public int* typesSizes;

    public int* typesOffsets;

    public int capacityInChunk;

    /// <summary>
    /// chunk指针数组
    /// </summary>
    public UnsafePtrList<Chunk> chunkData;

    public int EntityCount
    {
        get
        {
            int count = 0;

            foreach (var chunk in chunkData)
            {
                count += chunk->Size;
            }

            return count;
        }
    }


    public Ref<T> ResolvComponent<T>(int index) where T : unmanaged, IComponentData
    {
        if (index >= EntityCount)
        {
            //throw;
        }

        int chunckIndex = 0;

        while (index >= capacityInChunk)
        {
            chunckIndex++;
            index -= capacityInChunk;
        }

        int indexInTypesArray = TypeIndexInTypeArray<T>();

        T* components = (T*)chunkData[chunckIndex]->ResolvComponentOfOffset(typesOffsets[indexInTypesArray]);

        return new Ref<T>() { data = (components + index) };
    }

    public int TypeIndexInTypeArray<T>()
    {
        var typeIndex = SharedInstance<TypeManager>.Instance.TypeIndex<T>();

        for (int i = 0; i < typesCount; i++)
        {
            if (types[i].TypeIndex == typeIndex)
            {
                return i;
            }
        }

        return -1;
    }

    /**
     * 判断此原型是否包含所有类型
     */
    public bool IsContainAllTypes(ComponentType* componentTypes, int count)
    {
        for (int i = 0; i < count; i++)
        {
            bool c = false;
            for (int j = 0; j < typesCount; j++)
            {
                if ((componentTypes[i].typeIndex) == (types[j].TypeIndex))
                {
                    c = true;
                }
            }

            if (c == false)
            {
                return false;
            }
        }

        return true;
    }

    /**
     * 获取带有剩余空位的chunk
     */
    public Chunk* GetExistingChunkWithEmptySlots()
    {
        //如果已有chunk还有空间，则返回该chunk
        foreach (var chunkInData in chunkData)
        {
            if (chunkInData->Size < chunkInData->Capacity)
            {
                return chunkInData;
            }
        }

        return GetCleanChunk();
    }

    public void AddChunk(Chunk* chunk)
    {
        chunkData.Add(chunk);
    }

    public Chunk* GetCleanChunk()
    {
        var chunk = SharedInstance<ChunkPool>.Instance.GetChunk();

        chunk->ChunkState = ChunkState.Used;

        fixed (Archetype* ptr = &this)
        {
            chunk->Archetype = ptr;
            ptr->AddChunk(chunk);
        }

        return chunk;
    }
}