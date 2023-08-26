using TY.Time;

namespace TY.Worlds;

public partial class WorldManager
{
    /// <summary>
    /// 所有世界
    /// </summary>
    private readonly Dictionary<string, World> _worlds = new();
}

public partial class WorldManager
{
    public World CreateWorld(string name)
    {
        if (name == null) throw new ArgumentNullException(nameof(name));

        if (_worlds.TryGetValue(name, out var exists))
        {
            return exists;
        }

        var newWorld = new World
        {
            Name = name,
        };

        newWorld.AddSystem<TimeUpdateSystem>();

        return _worlds[name] = newWorld;
    }
}

public partial class WorldManager
{
    public void Update()
    {
        foreach (var (_, world) in _worlds)
        {
            world.Update();
        }
    }
}