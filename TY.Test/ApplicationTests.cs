using System.Threading.Tasks;
using NLog;
using TY.App;

namespace TY.Test;

public static class ApplicationTests
{
    private static readonly Logger Log = LogManager.GetCurrentClassLogger();

    public static async Task Main(string[] args)
    {
        var app = new Application();
        app.CreateNewWorld("Default World");
        await app.Run();
    }
}