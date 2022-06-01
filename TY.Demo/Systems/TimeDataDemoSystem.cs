using NLog;
using TY.Systems;

namespace TY.Demo.Systems;

public class TimeDataDemoSystem : SystemBase
{
    private readonly Logger _logger = LogManager.GetCurrentClassLogger();

    protected override void OnUpdate()
    {
        _logger.Debug(TimeData);
    }
}