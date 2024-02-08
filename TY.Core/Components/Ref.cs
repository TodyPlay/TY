using System.Runtime.CompilerServices;

namespace TY.Components;

public unsafe struct Ref<T> : IQueryTypeParameter where T : unmanaged, IQueryTypeParameter
{
    internal byte* _data;

    public ref T Value => ref Unsafe.AsRef<T>(_data);

    public static implicit operator T(Ref<T> t)
    {
        return Unsafe.AsRef<T>(t._data);
    }
}