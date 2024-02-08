using System;
using TY.Memory;

unsafe
{
    var ptr = MemoryUtility.AllocZeroed<int>(64);

    for (int i = 0; i < 64; i++)
    {
        ptr[i] = i;
    }

    MemoryUtility.Free(ptr);

    Console.WriteLine((int)ptr);
}