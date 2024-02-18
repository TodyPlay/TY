using System.Numerics;
using TY.Collections;
using TY.Components;

namespace TY.Demo.Components;

public struct PlayerInfo : IComponentData
{
    public long Id;

    public FixedString64 Name;

    public float Hp;

    public float Power;

    public Vector3 Position;
}