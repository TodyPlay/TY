using System.Runtime.InteropServices;
using TY.Collections;
using TY.Worlds;

namespace TY.Memory;

/// <summary>
/// 分配类型
/// </summary>
public enum AllocType
{
    /// <summary>
    /// 全局内存
    /// </summary>
    GLOBAL,

    /// <summary>
    /// （帧内存）每帧后释放 
    /// </summary>
    FRAME,
}

public class MemoryManager : IDisposable
{
    /// <summary>
    /// 帧内存指针列表，每帧完成后，释放内存。
    /// </summary>
    public UnsafeList<IntPtr> framePtrList;

    public MemoryManager()
    {
        WorldManager.OnFrameEnd += DoOnFrameEnd;
    }

    public void DoOnFrameEnd() => FreeFrameMemories();

    public void FreeFrameMemories()
    {
        foreach (var ptr in framePtrList)
        {
            unsafe
            {
                Free(ptr.ToPointer());
            }
        }

        framePtrList.Clear();
    }

    public void Dispose()
    {
        FreeFrameMemories();
    }

    public unsafe T* AllocZeroed<T>(int count = 1, AllocType allocType = AllocType.GLOBAL) where T : unmanaged
    {
        return (T*)AllocZeroed((uint)(sizeof(T) * count), allocType);
    }

    public unsafe void* AllocZeroed(uint size, AllocType allocType = AllocType.GLOBAL)
    {
        var ptr = NativeMemory.AllocZeroed(size);

        if (allocType == AllocType.FRAME)
        {
            framePtrList.Add(new IntPtr(ptr));
        }

        return ptr;
    }

    public unsafe void* Alloc(uint size, AllocType allocType = AllocType.GLOBAL)
    {
        var ptr = NativeMemory.Alloc(size);

        if (allocType == AllocType.FRAME)
        {
            framePtrList.Add(new IntPtr(ptr));
        }

        return ptr;
    }

    public unsafe void Free(void* ptr)
    {
        NativeMemory.Free(ptr);
    }

    public unsafe void Copy(void* source, void* destination, uint byteCount)
    {
        NativeMemory.Copy(source, destination, byteCount);
    }

    public unsafe void Copy(void* source, void* destination, int byteCount)
    {
        Copy(source, destination, (uint)byteCount);
    }

    public unsafe int MemoryCompare(void* ptr1, void* ptr2, int byteCount)
    {
        var s1 = new Span<byte>(ptr1, byteCount);
        var s2 = new Span<byte>(ptr2, byteCount);

        return s1.SequenceCompareTo(s2);
    }

    public unsafe void CleanMemory(void* chunk, uint size)
    {
        NativeMemory.Clear(chunk, size);
    }
}