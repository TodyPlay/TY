using System.Runtime.CompilerServices;
using TY.Collections;

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

    /**
     * 判断此原型是否包含所有类型
     */
    public bool IsContainAllTypes(ComponentType* componentTypes, int count)
    {
        if (count > typesCount)
        {
            return false;
        }
        
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