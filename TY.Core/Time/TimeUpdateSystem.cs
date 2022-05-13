using TY.Systems;

namespace TY.Time;

public sealed class TimeUpdateSystem : SystemBase
{
    private readonly DateTime _startTime = DateTime.Now;

    protected override Task OnUpdate()
    {
        Time.DeltaTime = (DateTime.Now - _startTime - TimeSpan.FromMilliseconds(Time.Time)).TotalMilliseconds;
        Time.Time = (DateTime.Now - _startTime).TotalMilliseconds;
        return Task.CompletedTask;
    }
}