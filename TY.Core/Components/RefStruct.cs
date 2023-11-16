using System.Runtime.CompilerServices;

namespace TY.Components;

public unsafe struct RefStruct<T> : IQueryTypeParameter where T : unmanaged, IQueryTypeParameter
{
    private byte* _data;

    public ref T Value => ref Unsafe.AsRef<T>(_data);
}