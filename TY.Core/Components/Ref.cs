using System.Runtime.CompilerServices;

namespace TY.Components;

public unsafe struct Ref<T> : IQueryTypeParameter where T : unmanaged, IQueryTypeParameter
{
    internal void* data;

    public ref T Value => ref Unsafe.AsRef<T>(data);

    public static implicit operator T(Ref<T> t)
    {
        return Unsafe.AsRef<T>(t.data);
    }
}