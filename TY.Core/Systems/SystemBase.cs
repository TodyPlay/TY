using TY.Entities;

namespace TY.Systems;

public abstract class SystemBase
{
    internal EntityManager _entityManager;

    protected EntityManager EntityManager => _entityManager;

    protected EntitiesForEach Entities => _entityManager.EntitiesForEach;

    public async Task Update()
    {
        await OnUpdate();
    }

    public virtual void Awake()
    {
    }

    protected virtual Task OnUpdate()
    {
        return Task.CompletedTask;
    }
}