using TY.Collections;
using TY.Entities;
using TY.Systems;
using TY.Worlds;

namespace TY.Unmanaged;

public struct WordUnmanaged
{
    public EntityManager entityManager;

    public World World;

    /// <summary>
    /// 非托管系统（暂未启用）
    /// </summary>
    public UnsafeList<SystemHandle> unmanagedSystems;
}