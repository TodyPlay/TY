using TY.Unmanaged;

namespace TY.Entities;

public unsafe struct EntityChunkInfo
{
    /// <summary>
    /// 该实体所在的块
    /// </summary>
    public Chunk* p;

    /// <summary>
    /// 该实体在chunk中的位置
    /// </summary>
    public int indexInChunk;
}