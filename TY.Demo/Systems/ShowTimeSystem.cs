using NLog;
using TY.Systems;

namespace TY.Demo.Systems;

public class ShowTimeSystem : SystemBase
{
    private Logger _logger = LogManager.GetCurrentClassLogger();

    private int _counter;
    
    protected override void OnUpdate()
    {
        if (++ _counter > 60)
        {
            _counter -= 60;
            _logger.Debug(TimeData);
        }
    }
}