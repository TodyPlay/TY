using NLog;
using TY.App;
using TY.Demo.Systems;
using TY.Network.systems;

namespace TY.Demo;

public static class Programmer
{
    private static readonly Logger Log = LogManager.GetCurrentClassLogger();

    public static void Main(string[] args)
    {
        var application = new Application();

        var world = application.CreateWorld();
        world.CreateAndGetSystem<NetworkSystem>();
        world.CreateAndGetSystem<PlayerSystem>();
        world.CreateAndGetSystem<ShowTimeSystem>();

        application.Run();
    }
}