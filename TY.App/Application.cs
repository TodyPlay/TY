using System.Reflection;
using NLog;
using TY.Worlds;

namespace TY.App;

public class Application : IDisposable
{
    private static readonly Logger Log = LogManager.GetCurrentClassLogger();

    private Assembly[] _assemblies;

    public bool Enable { get; set; } = true;

    public int Sleep { get; set; } = 1;

    public Application() : this(AppDomain.CurrentDomain.GetAssemblies())
    {
    }

    public Application(Assembly[] assemblies)
    {
        _assemblies = assemblies;
    }

    public void AddWorld(string name)
    {
        var world = new World(name, _assemblies);
        Log.Debug($"Create World:{world}");
    }

    public void Run()
    {
        while (Enable)
        {
            Thread.Sleep(Sleep);
            Update();
        }
    }

    public void Update()
    {
        foreach (var world in World.AllWorldsReadOnly)
        {
            world.Update();
        }
    }

    public void Dispose()
    {
        Enable = false;
        foreach (var world in World.AllWorldsReadOnly)
        {
            world.Dispose();
        }
    }
}