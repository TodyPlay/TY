using TY.Systems;

namespace TY.Time;

[SystemOrder(0)]
public sealed class TimeUpdateSystem : SystemBase
{
    private readonly DateTime _startTime = DateTime.Now;

    protected override Task OnUpdate()
    {
        TimeData.DeltaTime = (DateTime.Now - _startTime - TimeSpan.FromMilliseconds(TimeData.Time)).TotalMilliseconds;
        TimeData.Time = (DateTime.Now - _startTime).TotalMilliseconds;
        return Task.CompletedTask;
    }
}