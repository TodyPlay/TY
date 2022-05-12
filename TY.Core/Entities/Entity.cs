namespace TY.Entities;

/// <summary>
/// 实体实例
/// </summary>
public class Entity : IEquatable<Entity>
{
    private readonly uint _index;

    public uint Index => _index;

    public uint Version = 0;

    internal Entity(uint index)
    {
        _index = index;
    }

    public bool Equals(Entity? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return _index == other._index;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Entity) obj);
    }

    public override int GetHashCode()
    {
        return (int) _index;
    }

    public static bool operator ==(Entity? left, Entity? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Entity? left, Entity? right)
    {
        return !Equals(left, right);
    }
}