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
    public UnsafeList<TypeIndex> types;

    /// <summary>
    /// chunk指针数组
    /// </summary>
    public UnsafePtrList<Chunk> chunkData;
}