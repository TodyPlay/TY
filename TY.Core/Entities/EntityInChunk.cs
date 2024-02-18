using TY.Unmanaged;

namespace TY.Entities;

public unsafe struct EntityInChunk
{
    /// <summary>
    /// 该实体所在的块
    /// </summary>
    public Chunk* Chunk;

    /// <summary>
    /// 该实体在chunk中的位置
    /// </summary>
    public int IndexInChunk;
}