using System.Linq;
using NLog;
using TY.Systems;
using TY.Time;

namespace TY.Demo.Systems;

public class ShowTimeSystem : SystemBase
{
    public static Logger logger = LogManager.GetCurrentClassLogger();

    private double _counter;

    public override void Update()
    {
        TimeData timeData = EntityManager.Query<TimeData>().First();
        if ((_counter += timeData.DeltaTime) >= 1000)
        {
            _counter -= 1000;
        }
        logger.Debug(timeData);
        logger.Debug(_counter);
    }
}