using System;
using NLog;
using TY.App;
using TY.Components;
using TY.Entities;
using TY.Systems;

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

public class TestSystem : SystemBase
{
    private readonly Logger _logger = LogManager.GetCurrentClassLogger();

    public override void Awake()
    {
        var entity = EntityManager.CreateEntity();
        EntityManager.AddComponent(entity, new Data {X = 10, Y = 10});
    }


    public override void OnUpdate()
    {
        Entities.ForEach((Data d) => { Console.WriteLine(d); });
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