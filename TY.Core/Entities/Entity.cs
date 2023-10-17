namespace TY.Entities;

/// <summary>
///     实体实例
/// </summary>
public class Entity
{
    internal Entity()
    {
    }

    public uint Index { get; init; }

    public override string ToString()
    {
        return $"{nameof(Index)}: {Index}";
    }
}