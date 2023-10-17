using TY.Components;

namespace TY.Time;

public sealed class TimeData : ISharedComponentData
{
    public double Time { get; internal set; }

    public double DeltaTime { get; internal set; }

    public override string ToString()
    {
        return $"{nameof(Time)}: {Time}, {nameof(DeltaTime)}: {DeltaTime}";
    }
}