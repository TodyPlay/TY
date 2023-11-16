using NLog;
using TY.Entities;
using TY.Systems;
using TY.Unmanaged;

namespace TY.Worlds;

public class World
{
    private readonly Logger _logger = LogManager.GetCurrentClassLogger();

    private readonly Dictionary<Type, SystemBase> _systemLookup = new();
    private readonly List<SystemBase> _systems = new();

    private WordUnmanaged _wordUnmanaged;

    public World(string name)
    {
        Name = name;
        Init();
    }

    private void Init()
    {
        var entityManager = new EntityManager()
        {
            entityDataAccess = default,
        };

        _wordUnmanaged = new WordUnmanaged()
        {
            entityManager = entityManager,
            World = this,
            unmanagedSystems = default,
        };
    }

    internal string Name { get; init; }

    public override string ToString()
    {
        return $"World Name = {Name}";
    }

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
            try
            {
                system.Update();
            }
            catch (Exception e)
            {
                system.Enable = false;
                _logger.Error(e, e.Message);
            }
    }

    public T CreateAndGetSystem<T>() where T : SystemBase, new()
    {
        return CreateAndGetSystem(new T());
    }

    public T CreateAndGetSystem<T>(T system) where T : SystemBase
    {
        if (_systemLookup.TryGetValue(typeof(T), out var exists)) return (T)exists;

        system.WordUnmanaged = _wordUnmanaged;
        system.Awake();

        _systems.Add(system);
        _systemLookup[system.GetType()] = system;

        return system;
    }
}