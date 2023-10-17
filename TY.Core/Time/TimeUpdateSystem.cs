using TY.Systems;

namespace TY.Time;

public sealed class TimeUpdateSystem : SystemBase
{
    private readonly DateTime _startTime = DateTime.Now;

    public override int Order => 0;

    public override void Awake()
    {
        EntityManager.AddComponent<TimeData>(EntityManager.CreateEntity());
    }

    public override void Update()
    {
        foreach (var timeData in EntityManager.Query<TimeData>())
        {
            timeData.DeltaTime =
                (DateTime.Now - _startTime - TimeSpan.FromMilliseconds(timeData.Time)).TotalMilliseconds;
            timeData.Time = (DateTime.Now - _startTime).TotalMilliseconds;
        }
    }
}