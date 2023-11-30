using System.Runtime.InteropServices;

namespace TY.Memory;

public class Unsafe
{
    public static unsafe void* AllocZeroed(uint size)
    {
        return NativeMemory.AllocZeroed(size);
    }

    public static unsafe void* Alloc(uint size)
    {
        return NativeMemory.Alloc(size);
    }

    public static unsafe void Free(void* ptr)
    {
        NativeMemory.Free(ptr);
    }

    public static unsafe void Copy(void* source, void* destination, uint byteCount)
    {
        NativeMemory.Copy(source, destination, byteCount);
    }

    public static unsafe int MemoryCompare(void* ptr1, void* ptr2, int byteCount)
    {
        var s1 = new Span<byte>(ptr1, byteCount);
        var s2 = new Span<byte>(ptr2, byteCount);

        return s1.SequenceCompareTo(s2);
    }
}