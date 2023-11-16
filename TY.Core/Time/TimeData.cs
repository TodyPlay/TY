using TY.Components;

namespace TY.Time;

public struct TimeData : ISharedComponentData
{
    public double Time;

    public double DeltaTime;

    public override string ToString()
    {
        return $"{nameof(Time)}: {Time}, {nameof(DeltaTime)}: {DeltaTime}";
    }
}