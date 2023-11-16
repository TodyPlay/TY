using TY.Collections;
using TY.Components;
using TY.Unmanaged;

namespace TY.Entities;

public unsafe partial struct EntityManager
{
    public EntityDataAccess* entityDataAccess;

    public Entity CreateEntity()
    {
        throw new NotImplementedException();
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
public struct EntityDataAccess
{
    public UnsafeList<Archetype> archetypes;
}