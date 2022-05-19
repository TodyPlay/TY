using System.Threading.Tasks;
using NLog;
using TY.Systems;

namespace TY.Demo.Systems;

public class TimeDataDemoSystem : SystemBase
{
    private readonly Logger _logger = LogManager.GetCurrentClassLogger();

    protected override Task OnUpdate()
    {
        _logger.Debug(TimeData);
        return Task.CompletedTask;
    }
}