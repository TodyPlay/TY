using TY.Collections;
using TY.Components;

namespace TY.Unmanaged;

public class TypeManager
{
    private List<Type> _types = new();

    public TypeIndex TypeIndex<T>()
    {
        return TypeIndex(typeof(T));
    }

    public TypeIndex TypeIndex(Type type)
    {
        var index = _types.IndexOf(type);

        if (index == -1)
        {
            _types.Add(type);
            return new TypeIndex() { buffer = _types.Count - 1 };
        }

        return new TypeIndex() { buffer = index };
    }

    public Type GetType(int index)
    {
        return _types[index];
    }

    public Type GetType(TypeIndex typeIndex)
    {
        return _types[typeIndex.Index];
    }
}

public struct TypeIndex : IEquatable<TypeIndex>
{
    /// <summary>
    /// 数据buffer 000000000_00000000_00001111_11111111
    /// 用32位来存储数据，其中右边12位是类型的索引，剩下的是类型描述标记
    /// </summary>
    public int buffer;

    /// <summary>
    /// 类型的索引
    /// </summary>
    public int Index => buffer & INDEX_MASK;

    /// <summary>
    /// 类型最大支持的数量
    /// </summary>
    private const int INDEX_MAX = 1 << 13;

    /// <summary>
    /// 类型索引以外的标记清除
    /// </summary>
    private const int INDEX_MASK = INDEX_MAX - 1;


    public bool Equals(TypeIndex other)
    {
        return buffer == other.buffer;
    }

    public override bool Equals(object? obj)
    {
        return obj is TypeIndex other && Equals(other);
    }

    public override int GetHashCode()
    {
        return buffer;
    }

    public static bool operator ==(TypeIndex a, TypeIndex b)
    {
        return a.buffer == b.buffer;
    }

    public static bool operator !=(TypeIndex a, TypeIndex b)
    {
        return !(a == b);
    }
}

public struct ComponentType
{
    public TypeIndex typeIndex;

    public static ComponentType ReadWrite<T>() where T : unmanaged, IComponentData
    {
        return new ComponentType() { typeIndex = SharedInstance<TypeManager>.Instance.TypeIndex<T>() };
    }
}

public struct ComponentTypeInArchetype
{
    public TypeIndex TypeIndex;
}