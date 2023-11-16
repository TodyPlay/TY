namespace TY.Collections;

public unsafe struct NativeArray<T> where T : unmanaged
{
    private T* ptr;

    private int length;
}