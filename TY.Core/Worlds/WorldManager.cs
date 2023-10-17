using NLog;
using TY.Time;

namespace TY.Worlds;

public abstract class WorldManager
{
    private readonly Logger _logger = LogManager.GetCurrentClassLogger();

    /// <summary>
    ///     所有世界
    /// </summary>
    private readonly Dictionary<string, World> _worlds = new();

    public World CreateWorld()
    {
        return CreateWorld(Guid.NewGuid().ToString());
    }

    public World CreateWorld(string name)
    {
        if (name == null) throw new ArgumentNullException(nameof(name));

        if (_worlds.TryGetValue(name, out var exists)) return exists;

        var newWorld = new World(name);

        newWorld.CreateAndGetSystem<TimeUpdateSystem>();

        return _worlds[name] = newWorld;
    }

    public bool GetWorld(string name, out World? world)
    {
        return _worlds.TryGetValue(name, out world);
    }

    protected void Start()
    {
        _logger.Info("世界管理器启动");
        foreach (var world in _worlds.Values) world.Start();
    }

    protected void Update()
    {
        foreach (var world in _worlds.Values) world.Update();
    }
}