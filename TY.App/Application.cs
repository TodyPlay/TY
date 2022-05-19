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

    public int Delay { get; set; } = 0;

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

    public World CreateNewWorld(string name)
    {
        return new World(name, _assemblies);
    }

    public void Run()
    {
        while (Enable)
        {
            Thread.Sleep(Delay);
            Update();
        }
    }

    private void Update()
    {
        Task.WaitAll(World.AllWorldsReadOnly.Select(w => w.Update()).ToArray());
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