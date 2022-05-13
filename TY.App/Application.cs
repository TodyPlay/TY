using System.Reflection;
using NLog;
using TY.Worlds;

namespace TY.App;

public class Application : IDisposable
{
    private static readonly Logger Log = LogManager.GetCurrentClassLogger();

    private static readonly Assembly[] FrameworkAssemblies =
        {Assembly.Load("TY.Core"), Assembly.Load("TY.App")};

    private Assembly[] _assemblies;

    public bool Enable { get; set; } = true;

    public int Delay { get; set; } = 1;

    public Application() : this(AppDomain.CurrentDomain.GetAssemblies())
    {
    }

    public Application(Assembly[] assemblies)
    {
        var list = new List<Assembly>();
        list.AddRange(assemblies);
        list.AddRange(FrameworkAssemblies);
        _assemblies = list.Distinct().ToArray();
    }

    public void CreateNewWorld(string name)
    {
        var world = new World(name, _assemblies);
        Log.Debug($"Create World:{world}");
    }

    public async Task Run()
    {
        while (Enable)
        {
            await Task.Delay(Delay);
            await Update();
        }
    }

    private async Task Update()
    {
        foreach (var world in World.AllWorldsReadOnly)
        {
            await world.Update();
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