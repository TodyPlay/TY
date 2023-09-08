using TY.Entities;
using TY.Time;
using TY.Worlds;

namespace TY.Systems;

public abstract partial class SystemBase
{
    private EntityManager? _entityManager;

    public EntityManager EntityManager
    {
        get => _entityManager!;
        set => _entityManager = value;
    }

    protected EntitiesForEach Entities => EntityManager.EntitiesForEach;

    protected World World => EntityManager.World;

    protected TimeData TimeData => World.TimeData;


    public void Update()
    {
        OnUpdate();
    }

    public virtual void Awake()
    {
    }

    protected virtual void OnUpdate()
    {
    }
}

/// <summary>
/// 排序
/// </summary>
public partial class SystemBase
{
    public virtual int Order => int.MaxValue;
}