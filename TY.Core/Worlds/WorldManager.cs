using NLog;
using TY.Time;

namespace TY.Worlds;

public partial class WorldManager
{
    private Logger _logger = LogManager.GetCurrentClassLogger();

    /// <summary>
    /// 所有世界
    /// </summary>
    private readonly Dictionary<string, World> _worlds = new();
}

public partial class WorldManager
{
    public World CreateWorld()
    {
        return CreateWorld(Guid.NewGuid().ToString());
    }

    public World CreateWorld(string name)
    {
        if (name == null) throw new ArgumentNullException(nameof(name));

        if (_worlds.TryGetValue(name, out var exists))
        {
            return exists;
        }

        var newWorld = new World(name);

        newWorld.CreateAndGetSystem<TimeUpdateSystem>();

        return _worlds[name] = newWorld;
    }

    public World? GetWorld(string name)
    {
        return _worlds.TryGetValue(name, out var value) ? value : null;
    }
}

public partial class WorldManager
{
    protected void Start()
    {
        _logger.Info("世界管理器启动");

        foreach (var (_, world) in _worlds)
        {
            world.Start();
        }
    }

    protected void Update()
    {
        foreach (var (_, world) in _worlds)
        {
            world.Update();
        }
    }
}