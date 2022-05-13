using TY.Entities;
using TY.Time;
using TY.Worlds;

namespace TY.Systems;

public abstract class SystemBase
{
    public EntityManager EntityManager { get; internal set; } = null!;

    protected EntitiesForEach Entities => EntityManager.EntitiesForEach!;

    protected TimeData TimeData => EntityManager.World.TimeData;

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