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

    public static IReadOnlyList<World> AllWorldsReadOnly => new ReadOnlyCollection<World>(AllWorlds);

    public static event Action<World>? WorldCreated;

    public static event Action<World>? WorldDestroyed;

    public static event Action<World, SystemBase>? SystemCreated;

    public static event Action<World, SystemBase>? SystemDestroy;

    private bool _destroyed = false;

    private bool _enable;

    private List<SystemBase> _systems = new List<SystemBase>();

    private Dictionary<Type, SystemBase> _systemLookup = new Dictionary<Type, SystemBase>();

    private EntityManager _entityManager;

    public EntityManager? EntityManager => _entityManager;

    public string Name { get; }

    public WorldType WorldType { get; }

    private Assembly[] _assemblies;

    public override string ToString()
    {
        return Name;
    }

    public World(string name, WorldType worldType = WorldType.Default) :
        this(name, AppDomain.CurrentDomain.GetAssemblies(), worldType)
    {
    }

    public World(string name, Assembly[] assemblies, WorldType worldType = WorldType.Default)
    {
        Name = name;
        WorldType = worldType;
        _entityManager = new EntityManager(this);
        _assemblies = assemblies;
        Init();
    }

    private void Init()
    {
        AllWorlds.Add(this);
        var types = Utility.GetTypesFrom(typeof(SystemBase), _assemblies);
        foreach (var type in types)
        {
            var system = CreateSystemAndAdd(type);
            if (system != null)
            {
                system.World = this;
                system.Awake();
            }
        }

        WorldCreated?.Invoke(this);
        _enable = true;
    }

    public void Update()
    {
        if (_enable)
        {
            foreach (var system in _systems)
            {
                system.Update();
            }
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

    SystemBase? GetExistsSystem(Type type)
    {
        return _systemLookup.TryGetValue(type, out var systemBase) ? systemBase : null;
    }

    T? GetExistsSystem<T>() where T : SystemBase
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
            SystemCreated?.Invoke(this, system);
        }

        return system;
    }

    T? CreateSystemAndAdd<T>() where T : SystemBase
    {
        return (T?) CreateSystemAndAdd(typeof(T));
    }

    private SystemBase? CreateSystem(Type type)
    {
        return (SystemBase?) Activator.CreateInstance(type);
    }


    T CreateSystem<T>() where T : SystemBase
    {
        return Activator.CreateInstance<T>();
    }

    public void Dispose()
    {
        if (_destroyed)
        {
            return;
        }

        _destroyed = true;
        _enable = false;

        AllWorlds.Remove(this);
        WorldDestroyed?.Invoke(this);

        foreach (var systemBase in _systems)
        {
            systemBase.Destroy();
            SystemDestroy?.Invoke(this, systemBase);
        }

        _systems.Clear();
        _systems = null;

        _systemLookup.Clear();
        _systemLookup = null;

        _entityManager.Destroy();
        _entityManager = null;
    }
}