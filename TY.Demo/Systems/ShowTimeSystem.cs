using System.Linq;
using NLog;
using TY.Systems;
using TY.Time;

namespace TY.Demo.Systems;

public class ShowTimeSystem : SystemBase
{
    private double _counter;
    private readonly Logger _logger = LogManager.GetCurrentClassLogger();

    public override void Update()
    {
        var timeData = EntityManager.Query<TimeData>().First();
        if ((_counter += timeData.DeltaTime) >= 1000)
        {
            _counter -= 1000;
            _logger.Debug(timeData);
        }
    }
}