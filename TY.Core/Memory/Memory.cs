using System.Runtime.InteropServices;

namespace TY.Memory;

public static class Memory
{
    public static IntPtr Alloc(int size)
    {
        return Marshal.AllocHGlobal(size);
    }

    public static unsafe void MemorySet(void* ptr, int size)
    {
        throw new NotImplementedException();
    }
}