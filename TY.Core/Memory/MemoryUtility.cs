using System.Runtime.InteropServices;

namespace TY.Memory;

public enum AllocType
{
    /**
     * 全局内存
     */
    GLOBAL,

    /**
     * 每帧后释放
     */
    FRAME,
}

public class MemoryUtility
{
    public static unsafe T* AllocZeroed<T>(int count = 1 , AllocType allocType = AllocType.GLOBAL) where T : unmanaged
    {
        return (T*)AllocZeroed((uint)(sizeof(T) * count) , allocType);
    }

    public static unsafe void* AllocZeroed(uint size , AllocType allocType = AllocType.GLOBAL)
    {
        return NativeMemory.AllocZeroed(size);
    }

    public static unsafe void* Alloc(uint size, AllocType allocType = AllocType.GLOBAL)
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

    public static unsafe void Copy(void* source, void* destination, int byteCount)
    {
        Copy(source, destination, (uint)byteCount);
    }

    public static unsafe int MemoryCompare(void* ptr1, void* ptr2, int byteCount)
    {
        var s1 = new Span<byte>(ptr1, byteCount);
        var s2 = new Span<byte>(ptr2, byteCount);

        return s1.SequenceCompareTo(s2);
    }

    public static unsafe void CleanMemory(void* chunk, uint size)
    {
        NativeMemory.Clear(chunk, size);
    }
}