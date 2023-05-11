using System.Reflection;
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

    internal IEnumerable<Assembly> Assemblies { get; init; }

    public override string ToString()
    {
        return Name;
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
    private readonly List<SystemBase> _systems = new List<SystemBase>();

    private readonly Dictionary<Type, SystemBase> _systemLookup = new Dictionary<Type, SystemBase>();

    public void Awake()
    {
        foreach (var (type, system) in Utility.GetTypesFromAssembly(typeof(SystemBase), Assemblies)
                     .Distinct()
                     .Select(type => (type, CreateSystem(type)))
                     .Where(x => x.Item2 != null)
                     .OrderBy(x => x.Item2!.Order)
                )
        {
            system!.EntityManager = EntityManager;
            system.Awake();
            _systems.Add(system);
            _systemLookup[type] = system;
        }
    }
}

public partial class World
{
    public void Update()
    {
        foreach (var system in _systems)
        {
            system.Update();
        }
    }
}

public partial class World
{
    public SystemBase? GetOrCreateSystem(Type type)
    {
        var system = GetExistsSystem(type);
        return system ?? CreateSystemAndAdd(type);
    }

    public T? GetOrCreateSystem<T>() where T : SystemBase
    {
        var system = GetExistsSystem<T>();
        return system ?? CreateSystemAndAdd<T>();
    }

    private SystemBase? GetExistsSystem(Type type)
    {
        return _systemLookup.TryGetValue(type, out var systemBase) ? systemBase : null;
    }

    private T? GetExistsSystem<T>() where T : SystemBase
    {
        return (T?)GetExistsSystem(typeof(T));
    }

    private SystemBase? CreateSystemAndAdd(Type type)
    {
        var system = CreateSystem(type);
        if (system != null)
        {
            _systems.Add(system);
            _systemLookup[type] = system;
        }

        return system;
    }

    private T? CreateSystemAndAdd<T>() where T : SystemBase
    {
        return (T?)CreateSystemAndAdd(typeof(T));
    }

    private SystemBase? CreateSystem(Type type)
    {
        return Activator.CreateInstance(type) as SystemBase;
    }


    private T CreateSystem<T>() where T : SystemBase
    {
        return Activator.CreateInstance<T>();
    }
}