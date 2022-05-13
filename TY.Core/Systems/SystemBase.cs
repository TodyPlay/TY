using TY.Entities;
using TY.Time;

namespace TY.Systems;

public abstract class SystemBase
{
    public EntityManager EntityManager { get; internal set; } = null!;

    protected EntitiesForEach Entities => EntityManager.EntitiesForEach!;

    protected static TimeData Time { get; } = new();

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