namespace TY.Entities;

/// <summary>
/// 实体实例
/// </summary>
public class Entity
{
    public uint Index { get; init; }

    public uint Version { get; internal set; }

    internal Entity()
    {
    }

    public override string ToString()
    {
        return $"{nameof(Index)}: {Index}, {nameof(Version)}: {Version}";
    }
}