namespace TY.Collections;

public unsafe struct UnsafeList<T> where T : unmanaged
{
    private T* _ptr;

    private int _lenght;

    private int _capacity;
}

public unsafe struct UnsafePtrList<T> where T : unmanaged
{
    private T** ptr;

    private int _lenght;

    private int _capacity;
}

public unsafe struct UnsafePtrList
{
    private void** ptr;

    private int _lenght;

    private int _capacity;
}