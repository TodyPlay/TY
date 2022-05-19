using NLog;
using TY.App;

namespace TY.Test;

public static class ApplicationTests
{
    private static readonly Logger Log = LogManager.GetCurrentClassLogger();

    public static void Main(string[] args)
    {
        var app = new Application();
        app.CreateNewWorld("Default World");
        app.Run();
    }
}