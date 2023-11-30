using System.Runtime.InteropServices;
using System.Security.Cryptography;
using TY.Memory;

namespace TY.Unmanaged;

public static class ChunkPool
{
    private static Queue<nint> _chunks = new();

    private const int SIZE = 16 * 1024;

    public static unsafe Chunk* GetChunk()
    {
        if (_chunks.Count > 0)
        {
            return (Chunk*)_chunks.Dequeue();
        }

        return NewChunk();
    }

    public static unsafe void BackChunk(Chunk* chunk)
    {
        var ptr = Unsafe.AllocZeroed(SIZE);

        _chunks.Enqueue((IntPtr)ptr);
    }

    private static unsafe Chunk* NewChunk()
    {
        var chunk = Marshal.AllocHGlobal(SIZE);

        CryptographicOperations.ZeroMemory(new Span<byte>(chunk.ToPointer(), SIZE));

        return (Chunk*)chunk;
    }
}

public enum ChunkState : byte
{
    Unused = 0,
    Used = 1
}

/// <summary>
/// 一个块只存储一种原型，以此优化内存碎片。并且提高查询效率
/// </summary>
[StructLayout(LayoutKind.Explicit, Size = CHUNK_SIZE)]
public unsafe struct Chunk
{
    public override string ToString()
    {
        return
            $"{nameof(ChunkState)}: {ChunkState}, {nameof(Capacity)}: {Capacity}, {nameof(Size)}: {Size}";
    }

    /// <summary>
    /// 块大小16k
    /// </summary>
    private const int CHUNK_SIZE = 16 * 1024;

    /// <summary>
    /// 头信息64b
    /// </summary>
    private const int HEADER_SIZE = 64;

    /// <summary>
    /// 实际的实体数据大小
    /// </summary>
    private const int BUFFER_SIZE = CHUNK_SIZE - HEADER_SIZE;

    [FieldOffset(0)] public ChunkState ChunkState;

    /// <summary>
    /// 能容纳的实体数量
    /// </summary>
    [FieldOffset(1)] public int Capacity;

    /// <summary>
    /// 已存储的实体数量
    /// </summary>
    [FieldOffset(5)] public int Size;

    /// <summary>
    /// 原型信息
    /// </summary>
    [FieldOffset(9)] public Archetype* Archetype;

    [FieldOffset(HEADER_SIZE)] public fixed byte Buffer[BUFFER_SIZE];
}