using NLog;
using TY.Entities;
using TY.Worlds;

namespace TY.Systems;

public abstract class SystemBase
{
    private bool _enable;

    private EntityManager? _entityManager;
    private Logger? _logger;
    protected Logger Log => _logger ??= LogManager.GetLogger(GetType().FullName);

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