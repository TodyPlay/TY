using TY.Collections;

namespace TY.Unmanaged;

public class TypeManager
{
    private List<Type> _types = new List<Type>();

    public TypeIndex TypeIndex(Type type)
    {
        var index = _types.IndexOf(type);

        if (index == -1)
        {
            _types.Add(type);
            return _types.Count - 1;
        }

        return index;
    }

    public Type GetType(int index)
    {
        return _types[index];
    }
}

public struct TypeIndex
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

    public static implicit operator int(TypeIndex ti) => ti.buffer;

    public static implicit operator TypeIndex(int value) => new() { buffer = value };
}

public struct ComponentType
{
    public TypeIndex typeIndex;
    
    public static implicit operator ComponentType(Type type) =>
        new() { typeIndex = SharedInstance<TypeManager>.Instance.TypeIndex(type) };

    public static implicit operator Type(ComponentType ct) =>
        SharedInstance<TypeManager>.Instance.GetType(ct.typeIndex.Index);

    public static implicit operator int(ComponentType ct) => ct.typeIndex.buffer;

    public static implicit operator ComponentType(int value) => new() { typeIndex = value };
}

public struct ComponentTypeInArchetype
{

    public TypeIndex TypeIndex;

}