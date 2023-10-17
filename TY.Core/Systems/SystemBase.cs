using NLog;
using NLog.Fluent;
using TY.Entities;
using TY.Worlds;

namespace TY.Systems;

public abstract class SystemBase
{
    protected Logger Log = LogManager.GetCurrentClassLogger();

    private EntityManager? _entityManager;

    private bool _enable;

    public bool Enable
    {
        get => _enable;
        set
        {
            _enable = value;
            if (!value) Log.Warn($"System Disabled :{GetType()}");
        }
    }

    public EntityManager EntityManager
    {
        get => _entityManager!;
        set => _entityManager = value;
    }

    protected World World => EntityManager.World;

    public virtual int Order => int.MaxValue;

    /// <summary>
    ///     当成构造方法使用
    /// </summary>
    public virtual void Awake()
    {
    }

    /// <summary>
    ///     世界启动时调用
    /// </summary>
    public virtual void Start()
    {
    }

    public virtual void Update()
    {
    }
}