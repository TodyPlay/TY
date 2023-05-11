using NLog;
using TY.App;

namespace TY.Demo;

public static class Programmer
{
    private static readonly Logger Log = LogManager.GetCurrentClassLogger();

    public static void Main(string[] args)
    {
        var app = new Application();
        app.WorldManager.NewWorld("Default World");
        app.Run();
    }
}