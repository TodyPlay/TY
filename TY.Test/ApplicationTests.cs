using System.Threading.Tasks;
using NLog;
using TY.App;
using TY.Components;
using TY.Systems;

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

public class TestSystem : SystemBase
{
    private readonly Logger _logger = LogManager.GetCurrentClassLogger();

    public override void Awake()
    {
        var entity = EntityManager.CreateEntity();
        EntityManager.AddComponent(entity, new Data {X = 10, Y = 10});
    }


    protected override async Task OnUpdate()
    {
        await Entities.ForEach((Data d1) =>
        {
            d1.X++;
            d1.Y++;
            _logger.Debug(d1);
            return Task.CompletedTask;
        });
    }
}

public class Data : IComponent
{
    public int X;
    public int Y;

    public override string ToString()
    {
        return $"{nameof(X)}: {X}, {nameof(Y)}: {Y}";
    }
}