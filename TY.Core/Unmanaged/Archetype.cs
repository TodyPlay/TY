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

    /// <summary>
    /// chunk指针数组
    /// </summary>
    public UnsafeList<IntPtr> chunkData;
}

// public unsafe struct EntityArchetype
// {
//     public Archetype* Archetype;
//
//     public bool Valid => Archetype != null;
// }