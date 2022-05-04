using NLog;
using TY.App;
using TY.Components;
using TY.Systems;
using TY.Worlds;

namespace TY.Test;

public static class ApplicationTests
{
    private static readonly Logger Log = LogManager.GetCurrentClassLogger();

    public static void Main(string[] args)
    {
        World.WorldCreated += w => { Log.Debug($"创建：{w}"); };
        World.SystemCreated += (w, s) => { Log.Debug($"创建：{w},{s}"); };
        World.SystemDestroy += (w, s) => { Log.Debug($"销毁：{w}:{s}"); };
        World.WorldDestroyed += w => { Log.Debug($"销毁：{w}"); };
        var app = new Application();
        app.AddWorld("Default World");
        app.Run();
    }
}

public class MySystem : SystemBase
{
    private readonly Logger _logger = LogManager.GetCurrentClassLogger();

    public override void Awake()
    {
        var entity = EntityManager.CreateEntity();
        EntityManager.AddComponent(entity, new Data {X = 10, Y = 10});
    }

    public class Query
    {
        public Data data;
    }

    public override void OnUpdate()
    {
        var queries = EntityManager.Query<Query>();
        foreach (var query in queries)
        {
            query.data.X++;
            query.data.Y++;
            _logger.Debug($"data:{query.data}");
        }
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