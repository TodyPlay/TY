using TY.Systems;

namespace TY.Time;

public sealed class TimeUpdateSystem : SystemBase
{
    private readonly DateTime _startTime = DateTime.Now;

    public override void Update()
    {
        TimeData.DeltaTime = (DateTime.Now - _startTime - TimeSpan.FromMilliseconds(TimeData.Time)).TotalMilliseconds;
        TimeData.Time = (DateTime.Now - _startTime).TotalMilliseconds;
    }

    public override int Order => 0;
}