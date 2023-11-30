using System.Collections;
using TY.Memory;

namespace TY.Collections;

#region UnsafePtrList

public unsafe struct UnsafePtrList<T> : IEnumerable<IntPtr> where T : unmanaged
{
    private T** _ptr;

    private int _length;

    private int _capacity;

    public int Length
    {
        get => _length;
        set
        {
            if (value > _capacity)
                Resize(value);
            else
                _length = value;
        }
    }

    private void Resize(int length)
    {
        if (length > Capacity) SetCapacity(length);

        _length = length;
    }

    private void SetCapacity(int capacity)
    {
        if (capacity < 0 || capacity < Length) throw new ArgumentOutOfRangeException();

        ResizeExact(Ceilpow2(capacity));
    }

    private void ResizeExact(int newCapacity)
    {
        if (newCapacity < 0) newCapacity = 0;

        T** newPtr = default;
        var size = sizeof(T);

        if (newCapacity > 0)
        {
            newPtr = (T**)Unsafe.AllocZeroed((uint)(newCapacity * size));
            if (_ptr != null && _capacity > 0)
            {
                var sizeToCopy = Math.Min(_capacity, newCapacity) * size;
                Unsafe.Copy(_ptr, newPtr, (uint)sizeToCopy);
            }
        }

        Unsafe.Free(_ptr);

        _ptr = newPtr;
        _capacity = newCapacity;
        _length = Math.Min(_length, newCapacity);
    }

    /// <summary>
    /// 获取大于或等于capacity的最小2的幂
    /// </summary>
    /// <param name="capacity">新的空间</param>
    /// <returns>大于或等于capacity的最小2的幂</returns>
    private static int Ceilpow2(int capacity)
    {
        capacity -= 1;
        capacity |= capacity >> 1;
        capacity |= capacity >> 2;
        capacity |= capacity >> 4;
        capacity |= capacity >> 8;
        capacity |= capacity >> 16;
        return capacity + 1;
    }

    public int Capacity
    {
        get => _capacity;
        set => SetCapacity(value);
    }

    public T* this[int index]
    {
        get => _ptr[CheckIndexRange(index)];
        set => _ptr[CheckIndexRange(index)] = value;
    }

    public ref T* ElementAt(int index)
    {
        return ref _ptr[CheckIndexRange(index)];
    }

    /// <summary>
    /// 检查索引的范围
    /// </summary>
    /// <param name="index">索引</param>
    /// <returns>索引</returns>
    /// <exception cref="ArgumentOutOfRangeException">检查未通过时抛出</exception>
    private int CheckIndexRange(int index)
    {
        if (index >= Length) throw new ArgumentOutOfRangeException();

        return index;
    }

    public void Add(in T* value)
    {
        var idx = _length;
        if (_length < _capacity)
        {
            _ptr[idx] = value;
            _length++;
        }
        else
        {
            Resize(idx + 1);
            _ptr[idx] = value;
        }
    }

    public void AddRange(T** ptr, int count)
    {
        var idx = _length;

        if (_length + count > Capacity)
            Resize(_length + count);
        else
            _length += count;

        var sizeOf = sizeof(T);
        void* dst = (byte*)_ptr + idx * sizeOf;

        Unsafe.Copy(ptr, dst, (uint)(count + sizeOf));
    }

    public void AddRange(UnsafePtrList<T> list)
    {
        AddRange(list._ptr, list.Length);
    }


    public void RemoveAt(int index)
    {
        CheckIndexRange(index);

        var dst = _ptr + index;
        var src = dst + 1;
        _length--;

        Unsafe.Copy(src, dst, (uint)(_length - index));
    }

    public readonly bool IsEmpty => !IsCreated || _length == 0;

    public readonly bool IsCreated => _ptr != null;

    public void Dispose()
    {
        if (!IsCreated) return;

        Unsafe.Free(_ptr);
        _ptr = default;
        _length = default;
        _capacity = default;
    }

    public void Clear()
    {
        _length = 0;
    }

    public Enumerator GetEnumerator()
    {
        return new Enumerator { m_Ptr = _ptr, m_Length = Length, m_Index = -1 };
    }


    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator<IntPtr> IEnumerable<IntPtr>.GetEnumerator()
    {
        throw new NotImplementedException();
    }

    public struct Enumerator : IEnumerator<IntPtr>
    {
        internal T** m_Ptr;
        internal int m_Length;
        internal int m_Index;

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            return ++m_Index < m_Length;
        }

        public void Reset()
        {
            m_Index = -1;
        }

        public IntPtr Current => (IntPtr)m_Ptr[m_Index];

        object IEnumerator.Current => Current;
    }
}

#endregion

#region UnsafeList

public unsafe struct UnsafeList<T> : IDisposable, INativeList<T>, IEnumerable<T> where T : unmanaged
{
    private T* _ptr;

    private int _length;

    private int _capacity;

    public int Length
    {
        get => _length;
        set
        {
            if (value > _capacity)
                Resize(value);
            else
                _length = value;
        }
    }

    private void Resize(int length)
    {
        if (length > Capacity) SetCapacity(length);

        _length = length;
    }

    private void SetCapacity(int capacity)
    {
        if (capacity < 0 || capacity < Length) throw new ArgumentOutOfRangeException();

        ResizeExact(Ceilpow2(capacity));
    }

    private void ResizeExact(int newCapacity)
    {
        if (newCapacity < 0) newCapacity = 0;

        T* newPtr = default;
        var size = sizeof(T);

        if (newCapacity > 0)
        {
            newPtr = (T*)Unsafe.AllocZeroed((uint)(newCapacity * size));
            if (_ptr != null && _capacity > 0)
            {
                var sizeToCopy = Math.Min(_capacity, newCapacity) * size;
                Unsafe.Copy(_ptr, newPtr, (uint)sizeToCopy);
            }
        }

        Unsafe.Free(_ptr);

        _ptr = newPtr;
        _capacity = newCapacity;
        _length = Math.Min(_length, newCapacity);
    }

    /// <summary>
    /// 获取大于或等于capacity的最小2的幂
    /// </summary>
    /// <param name="capacity">新的空间</param>
    /// <returns>大于或等于capacity的最小2的幂</returns>
    private static int Ceilpow2(int capacity)
    {
        capacity -= 1;
        capacity |= capacity >> 1;
        capacity |= capacity >> 2;
        capacity |= capacity >> 4;
        capacity |= capacity >> 8;
        capacity |= capacity >> 16;
        return capacity + 1;
    }

    public int Capacity
    {
        get => _capacity;
        set => SetCapacity(value);
    }

    public T this[int index]
    {
        get => _ptr[CheckIndexRange(index)];
        set => _ptr[CheckIndexRange(index)] = value;
    }

    public ref T ElementAt(int index)
    {
        return ref _ptr[CheckIndexRange(index)];
    }

    /// <summary>
    /// 检查索引的范围
    /// </summary>
    /// <param name="index">索引</param>
    /// <returns>索引</returns>
    /// <exception cref="ArgumentOutOfRangeException">检查未通过时抛出</exception>
    private int CheckIndexRange(int index)
    {
        if (index >= Length) throw new ArgumentOutOfRangeException();

        return index;
    }

    public void Add(in T value)
    {
        var idx = _length;
        if (_length < _capacity)
        {
            _ptr[idx] = value;
            _length++;
        }
        else
        {
            Resize(idx + 1);
            _ptr[idx] = value;
        }
    }

    public void AddRange(T* ptr, int count)
    {
        var idx = _length;

        if (_length + count > Capacity)
            Resize(_length + count);
        else
            _length += count;

        var sizeOf = sizeof(T);
        void* dst = (byte*)_ptr + idx * sizeOf;

        Unsafe.Copy(ptr, dst, (uint)(count + sizeOf));
    }

    public void AddRange(UnsafeList<T> list)
    {
        AddRange(list._ptr, list.Length);
    }


    public void RemoveAt(int index)
    {
        CheckIndexRange(index);

        var dst = _ptr + index;
        var src = dst + 1;
        _length--;

        Unsafe.Copy(src, dst, (uint)(_length - index));
    }

    public readonly bool IsEmpty => !IsCreated || _length == 0;

    public readonly bool IsCreated => _ptr != null;

    public void Dispose()
    {
        if (!IsCreated) return;

        Unsafe.Free(_ptr);
        _ptr = default;
        _length = default;
        _capacity = default;
    }

    public void Clear()
    {
        _length = 0;
    }

    public Enumerator GetEnumerator()
    {
        return new Enumerator { m_Ptr = _ptr, m_Length = Length, m_Index = -1 };
    }


    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
        throw new NotImplementedException();
    }

    public struct Enumerator : IEnumerator<T>
    {
        internal T* m_Ptr;
        internal int m_Length;
        internal int m_Index;

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            return ++m_Index < m_Length;
        }

        public void Reset()
        {
            m_Index = -1;
        }

        public T Current => m_Ptr[m_Index];

        object IEnumerator.Current => Current;
    }
}

#endregion
