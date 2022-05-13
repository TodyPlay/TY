using System.Collections.ObjectModel;
using System.Reflection;
using NLog;
using TY.Entities;
using TY.Systems;

namespace TY.Worlds;

[Flags]
public enum WorldType
{
    Default = 0
}

public partial class World : IDisposable
{
    private Logger _logger = LogManager.GetCurrentClassLogger();

    private static readonly List<World> AllWorlds = new List<World>();

    public static IEnumerable<World> AllWorldsReadOnly => new ReadOnlyCollection<World>(AllWorlds);

    private readonly List<SystemBase> _systems = new List<SystemBase>();

    private readonly Dictionary<Type, SystemBase> _systemLookup = new Dictionary<Type, SystemBase>();

    private EntityManager? _entityManager;

    private EntityManager EntityManager => _entityManager ??= new EntityManager();

    private string Name { get; }

    private readonly Assembly[] _assemblies;

    public override string ToString()
    {
        return Name;
    }

    public World(string name) :
        this(name, AppDomain.CurrentDomain.GetAssemblies())
    {
    }

    public World(string name, Assembly[] assemblies)
    {
        Name = name;
        _assemblies = assemblies;
        Init();
    }

    private void Init()
    {
        AllWorlds.Add(this);
        var types = Utility.GetTypesFromAssembly(typeof(SystemBase), _assemblies);
        foreach (var type in types)
        {
            var system = CreateSystemAndAdd(type);
            if (system != null)
            {
                system._entityManager = EntityManager;
                system.Awake();
            }
        }
    }

    public void Update()
    {
        foreach (var system in _systems)
        {
            system.Update();
        }
    }

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
        return (T?) GetExistsSystem(typeof(T));
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
        return (T?) CreateSystemAndAdd(typeof(T));
    }

    private SystemBase? CreateSystem(Type type)
    {
        return (SystemBase?) Activator.CreateInstance(type);
    }


    private T CreateSystem<T>() where T : SystemBase
    {
        return Activator.CreateInstance<T>();
    }

    public void Dispose()
    {
        AllWorlds.Remove(this);
    }
}