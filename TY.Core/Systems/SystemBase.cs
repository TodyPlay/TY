using TY.Entities;
using TY.Time;
using TY.Worlds;

namespace TY.Systems;

public abstract partial class SystemBase
{
    private EntityManager? _entityManager;

    private bool _enable;

    public bool Enable
    {
        get => _enable;
        set => _enable = value;
    }

    public EntityManager EntityManager
    {
        get => _entityManager!;
        set => _entityManager = value;
    }

    protected EntitiesForEach Entities => EntityManager.EntitiesForEach;

    protected World World => EntityManager.World;

    protected TimeData TimeData => World.TimeData;


    internal void Update()
    {
        OnUpdate();
    }

    /// <summary>
    /// 当成构造方法使用
    /// </summary>
    public virtual void Awake()
    {
    }

    /// <summary>
    /// 世界启动时调用
    /// </summary>
    public virtual void Start()
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