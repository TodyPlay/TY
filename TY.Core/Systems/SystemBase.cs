using TY.Entities;

namespace TY.Systems;

public abstract class SystemBase
{
    internal EntityManager _entityManager;

    protected EntityManager EntityManager => _entityManager;

    protected EntitiesForEach Entities => _entityManager.EntitiesForEach;

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