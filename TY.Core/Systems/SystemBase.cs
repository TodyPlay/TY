using System.Reflection;
using TY.Entities;
using TY.Time;

namespace TY.Systems;

public abstract partial class SystemBase
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

public partial class SystemBase : IComparable<SystemBase>
{
    public int CompareTo(SystemBase? other)
    {
        var t = GetType().GetCustomAttribute<SystemOrderAttribute>()?.Order ?? int.MaxValue;
        var o = other?.GetType().GetCustomAttribute<SystemOrderAttribute>()?.Order ?? int.MaxValue;
        return t.CompareTo(o);
    }
}