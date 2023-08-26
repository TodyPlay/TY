using TY.Entities;
using TY.Time;

namespace TY.Systems;

public abstract partial class SystemBase
{
    private EntityManager? _entityManager;

    public EntityManager EntityManager
    {
        get => _entityManager!;
        set => _entityManager = value;
    }

    protected EntitiesForEach Entities => EntityManager!.EntitiesForEach!;

    protected TimeData TimeData => EntityManager!.World.TimeData;

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

public partial class SystemBase
{
    public virtual int Order => int.MaxValue;
}