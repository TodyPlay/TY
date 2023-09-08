using System.Numerics;
using TY.Components;

namespace TY.Demo.Components;

public class PlayerInfo : IComponentData
{
    public long Id;

    public string? Name;

    public float Hp;

    public float Power;

    public Vector3 Position;
}