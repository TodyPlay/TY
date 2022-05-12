using TY.Entities;

namespace TY.Systems;

public abstract class SystemBase
{
    internal EntityManager _entityManager;

    public EntityManager EntityManager => _entityManager;

    public EntitiesForEach Entities => _entityManager.EntitiesForEach;

    public void Update()
    {
        OnUpdate();
    }

    public virtual void Awake()
    {
    }

    public virtual void OnUpdate()
    {
    }
}