using TY.Components;
using TY.Systems;

namespace TY.Time;

public sealed class TimeUpdateSystem : SystemBase
{
    private readonly DateTime _startTime = DateTime.Now;

    public override int Order => 0;

    public override void Awake()
    {
        EntityManager.CreateEntity(typeof(TimeData));
    }

    public override void Update()
    {
        foreach (var timeData in EntityManager.Query<RefStruct<TimeData>>())
        {
            timeData.Value.DeltaTime =
                (DateTime.Now - _startTime - TimeSpan.FromMilliseconds(timeData.Value.Time)).TotalMilliseconds;
            timeData.Value.Time = (DateTime.Now - _startTime).TotalMilliseconds;
        }
    }
}