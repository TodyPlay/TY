using NLog;
using TY.Entities;
using TY.Systems;
using TY.Time;

namespace TY.Worlds;

/// <summary>
/// 初始化数据
/// </summary>
public partial class World
{
    private Logger _logger = LogManager.GetCurrentClassLogger();

    internal string Name { get; init; }

    public override string ToString()
    {
        return Name;
    }

    public World(string name)
    {
        Name = name;
    }
}

public partial class World
{
    internal TimeData TimeData { get; } = new();
    private EntityManager? _entityManager;
    private EntityManager EntityManager => _entityManager ??= new EntityManager(this);
}

public partial class World
{
    private readonly List<SystemBase> _systems = new();

    private readonly Dictionary<Type, SystemBase> _systemLookup = new();
}

public partial class World
{
    public void Start()
    {
        foreach (var system in _systems.OrderBy(v => v.Order))
        {
            system.Start();
            system.Enable = true;
        }
    }

    public void Update()
    {
        foreach (var system in _systems.Where(v => v.Enable).OrderBy(v => v.Order))
        {
            try
            {
                system.Update();
            }
            catch (Exception e)
            {
                _logger.Error(e);
                system.Enable = false;
            }
        }
    }
}

public partial class World
{
    public T CreateAndGetSystem<T>() where T : SystemBase, new()
    {
        return CreateAndGetSystem(new T());
    }

    public T CreateAndGetSystem<T>(T system) where T : SystemBase
    {
        if (_systemLookup.TryGetValue(typeof(T), out var exists))
        {
            return (T)exists;
        }

        system.EntityManager = EntityManager;
        system.Awake();

        _systems.Add(system);
        _systemLookup[typeof(T)] = system;

        return system;
    }
}