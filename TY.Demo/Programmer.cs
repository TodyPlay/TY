using System.Threading.Tasks;
using NLog;
using TY.App;
using TY.Demo.Systems;

namespace TY.Demo;

public static class Programmer
{
    private static readonly Logger Log = LogManager.GetCurrentClassLogger();

    public static void Main(string[] args)
    {
        var app = new Application();
        var world = app.CreateWorld("Default World");
        world.AddSystem<DataDemoSystem>();
        world.AddSystem<TimeDataDemoSystem>();

        Task.Run(() => { app.Run(); });
    }
}