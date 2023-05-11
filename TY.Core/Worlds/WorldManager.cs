using System.Reflection;

namespace TY.Worlds;

public partial class WorldManager
{
    /// <summary>
    /// 所有世界
    /// </summary>
    public Dictionary<string, World> Worlds = new Dictionary<string, World>();

    /// <summary>
    /// 框架程序集
    /// </summary>
    private static readonly string[] FrameworkAssemblies =
    {
        "TY.Core"
    };
}

public partial class WorldManager
{
    public World NewWorld(string name)
    {
        return NewWorld(name, AppDomain.CurrentDomain.GetAssemblies());
    }

    public World NewWorld(string name, IEnumerable<Assembly> assemblies)
    {
        if (name == null) throw new ArgumentNullException(nameof(name));

        if (Worlds.TryGetValue(name, out var exists))
        {
            return exists;
        }

        var newWorld = new World
        {
            Name = name,
            Assemblies = FrameworkAssemblies.Select(Assembly.Load).Concat(assemblies),
        };

        newWorld.Awake();

        return Worlds[name] = newWorld;
    }
}

public partial class WorldManager
{
    public void Update()
    {
        foreach (var (_, world) in Worlds)
        {
            world.Update();
        }
    }
}