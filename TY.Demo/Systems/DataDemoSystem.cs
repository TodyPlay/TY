using System.Threading.Tasks;
using NLog;
using TY.Components;
using TY.Systems;

namespace TY.Demo.Systems;

public class DataDemoSystem : SystemBase
{
    private readonly Logger _logger = LogManager.GetCurrentClassLogger();

    public override void Awake()
    {
        var entity = EntityManager.CreateEntity();
        EntityManager.AddComponent(entity, new Data { X = 10, Y = 10 });
    }


    protected override void OnUpdate()
    {
        Entities.ForEach((Data d1) =>
        {
            d1.X++;
            d1.Y++;
            _logger.Debug(d1);
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