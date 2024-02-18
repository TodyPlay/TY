using TY.Entities;
using TY.Unmanaged;
using TY.Worlds;

namespace TY.Systems;

public abstract class SystemBase
{
    private bool _enable;

    public bool Enable
    {
        get => _enable;
        set => _enable = value;
    }

    public WordUnmanaged WordUnmanaged;

    public EntityManager EntityManager => WordUnmanaged.entityManager;

    public World World => WordUnmanaged.World;


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