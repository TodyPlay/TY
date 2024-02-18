using System.Collections;
using TY.Collections;
using TY.Components;
using TY.Unmanaged;

namespace TY.Entities;

public struct Enumerable<T1> : IEnumerable<Ref<T1>>
    where T1 : unmanaged, IComponentData
{
    public unsafe UnsafePtrList<Archetype>* matchingTypes;

    public Enumerator<T1> GetEnumerator()
    {
        unsafe
        {
            return new Enumerator<T1>()
                { entityIndex = -1, archetypeIndex = 0, matchingTypes = this.matchingTypes };
        }
    }

    IEnumerator<Ref<T1>> IEnumerable<Ref<T1>>.GetEnumerator()
    {
        return GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public struct Enumerator<T1> : IEnumerator<Ref<T1>>
    where T1 : unmanaged, IComponentData
{
    public int entityIndex;

    public int archetypeIndex;

    public unsafe UnsafePtrList<Archetype>* matchingTypes;

    public bool MoveNext()
    {
        unsafe
        {
            if (matchingTypes->Length == 0)
            {
                return false;
            }

            if (entityIndex + 1 < (*matchingTypes)[archetypeIndex]->EntityCount)
            {
                entityIndex++;
                return true;
            }

            if (archetypeIndex + 1 < (*matchingTypes).Length)
            {
                entityIndex = 0;
                archetypeIndex++;
                return true;
            }

            return false;
        }
    }

    public void Reset()
    {
        entityIndex = default;
    }

    public Ref<T1> Current
    {
        get
        {
            unsafe
            {
                return (
                    (*matchingTypes)[archetypeIndex]->ResolvComponent<T1>(entityIndex)
                );
            }
        }
    }

    object IEnumerator.Current => Current;

    public void Dispose()
    {
    }
}

public struct Enumerable<T1, T2> : IEnumerable<(Ref<T1>, Ref<T2>)>
    where T1 : unmanaged, IComponentData
    where T2 : unmanaged, IComponentData
{
    public unsafe UnsafePtrList<Archetype>* matchingTypes;

    public Enumerator<T1, T2> GetEnumerator()
    {
        unsafe
        {
            return new Enumerator<T1, T2>()
                { entityIndex = -1, archetypeIndex = 0, matchingTypes = this.matchingTypes };
        }
    }

    IEnumerator<(Ref<T1>, Ref<T2>)> IEnumerable<(Ref<T1>, Ref<T2>)>.GetEnumerator()
    {
        return GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public struct Enumerator<T1, T2> : IEnumerator<(Ref<T1>, Ref<T2>)>
    where T1 : unmanaged, IComponentData
    where T2 : unmanaged, IComponentData
{
    public int entityIndex;

    public int archetypeIndex;

    public unsafe UnsafePtrList<Archetype>* matchingTypes;

    public bool MoveNext()
    {
        unsafe
        {
            if (matchingTypes->Length == 0)
            {
                return false;
            }

            if (entityIndex + 1 < (*matchingTypes)[archetypeIndex]->EntityCount)
            {
                entityIndex++;
                return true;
            }

            if (archetypeIndex + 1 < (*matchingTypes).Length)
            {
                entityIndex = 0;
                archetypeIndex++;
                return true;
            }

            return false;
        }
    }

    public void Reset()
    {
        entityIndex = default;
    }

    public (Ref<T1>, Ref<T2>) Current
    {
        get
        {
            unsafe
            {
                return (
                    (*matchingTypes)[archetypeIndex]->ResolvComponent<T1>(entityIndex),
                    (*matchingTypes)[archetypeIndex]->ResolvComponent<T2>(entityIndex)
                );
            }
        }
    }

    object IEnumerator.Current => Current;

    public void Dispose()
    {
    }
}

public struct Enumerable<T1, T2, T3> : IEnumerable<(Ref<T1>, Ref<T2>, Ref<T3>)>
    where T1 : unmanaged, IComponentData
    where T2 : unmanaged, IComponentData
    where T3 : unmanaged, IComponentData
{
    public unsafe UnsafePtrList<Archetype>* matchingTypes;

    public Enumerator<T1, T2, T3> GetEnumerator()
    {
        unsafe
        {
            return new Enumerator<T1, T2, T3>()
                { entityIndex = -1, archetypeIndex = 0, matchingTypes = this.matchingTypes };
        }
    }

    IEnumerator<(Ref<T1>, Ref<T2>, Ref<T3>)> IEnumerable<(Ref<T1>, Ref<T2>, Ref<T3>)>.GetEnumerator()
    {
        return GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public struct Enumerator<T1, T2, T3> : IEnumerator<(Ref<T1>, Ref<T2>, Ref<T3>)>
    where T1 : unmanaged, IComponentData
    where T2 : unmanaged, IComponentData
    where T3 : unmanaged, IComponentData
{
    public int entityIndex;

    public int archetypeIndex;

    public unsafe UnsafePtrList<Archetype>* matchingTypes;

    public bool MoveNext()
    {
        unsafe
        {
            if (matchingTypes->Length == 0)
            {
                return false;
            }

            if (entityIndex + 1 < (*matchingTypes)[archetypeIndex]->EntityCount)
            {
                entityIndex++;
                return true;
            }

            if (archetypeIndex + 1 < (*matchingTypes).Length)
            {
                entityIndex = 0;
                archetypeIndex++;
                return true;
            }

            return false;
        }
    }

    public void Reset()
    {
        entityIndex = default;
    }

    public (Ref<T1>, Ref<T2>, Ref<T3>) Current
    {
        get
        {
            unsafe
            {
                return (
                    (*matchingTypes)[archetypeIndex]->ResolvComponent<T1>(entityIndex),
                    (*matchingTypes)[archetypeIndex]->ResolvComponent<T2>(entityIndex),
                    (*matchingTypes)[archetypeIndex]->ResolvComponent<T3>(entityIndex)
                );
            }
        }
    }

    object IEnumerator.Current => Current;

    public void Dispose()
    {
    }
}

public struct Enumerable<T1, T2, T3, T4> : IEnumerable<(Ref<T1>, Ref<T2>, Ref<T3>, Ref<T4>)>
    where T1 : unmanaged, IComponentData
    where T2 : unmanaged, IComponentData
    where T3 : unmanaged, IComponentData
    where T4 : unmanaged, IComponentData
{
    public unsafe UnsafePtrList<Archetype>* matchingTypes;

    public Enumerator<T1, T2, T3, T4> GetEnumerator()
    {
        unsafe
        {
            return new Enumerator<T1, T2, T3, T4>()
                { entityIndex = -1, archetypeIndex = 0, matchingTypes = this.matchingTypes };
        }
    }

    IEnumerator<(Ref<T1>, Ref<T2>, Ref<T3>, Ref<T4>)> IEnumerable<(Ref<T1>, Ref<T2>, Ref<T3>, Ref<T4>)>.GetEnumerator()
    {
        return GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public struct Enumerator<T1, T2, T3, T4> : IEnumerator<(Ref<T1>, Ref<T2>, Ref<T3>, Ref<T4>)>
    where T1 : unmanaged, IComponentData
    where T2 : unmanaged, IComponentData
    where T3 : unmanaged, IComponentData
    where T4 : unmanaged, IComponentData
{
    public int entityIndex;

    public int archetypeIndex;

    public unsafe UnsafePtrList<Archetype>* matchingTypes;

    public bool MoveNext()
    {
        unsafe
        {
            if (matchingTypes->Length == 0)
            {
                return false;
            }

            if (entityIndex + 1 < (*matchingTypes)[archetypeIndex]->EntityCount)
            {
                entityIndex++;
                return true;
            }

            if (archetypeIndex + 1 < (*matchingTypes).Length)
            {
                entityIndex = 0;
                archetypeIndex++;
                return true;
            }

            return false;
        }
    }

    public void Reset()
    {
        entityIndex = default;
    }

    public (Ref<T1>, Ref<T2>, Ref<T3>, Ref<T4>) Current
    {
        get
        {
            unsafe
            {
                return (
                    (*matchingTypes)[archetypeIndex]->ResolvComponent<T1>(entityIndex),
                    (*matchingTypes)[archetypeIndex]->ResolvComponent<T2>(entityIndex),
                    (*matchingTypes)[archetypeIndex]->ResolvComponent<T3>(entityIndex),
                    (*matchingTypes)[archetypeIndex]->ResolvComponent<T4>(entityIndex)
                );
            }
        }
    }

    object IEnumerator.Current => Current;

    public void Dispose()
    {
    }
}

public struct Enumerable<T1, T2, T3, T4, T5> : IEnumerable<(Ref<T1>, Ref<T2>, Ref<T3>, Ref<T4>, Ref<T5>)>
    where T1 : unmanaged, IComponentData
    where T2 : unmanaged, IComponentData
    where T3 : unmanaged, IComponentData
    where T4 : unmanaged, IComponentData
    where T5 : unmanaged, IComponentData
{
    public unsafe UnsafePtrList<Archetype>* matchingTypes;

    public Enumerator<T1, T2, T3, T4, T5> GetEnumerator()
    {
        unsafe
        {
            return new Enumerator<T1, T2, T3, T4, T5>()
                { entityIndex = -1, archetypeIndex = 0, matchingTypes = this.matchingTypes };
        }
    }

    IEnumerator<(Ref<T1>, Ref<T2>, Ref<T3>, Ref<T4>, Ref<T5>)>
        IEnumerable<(Ref<T1>, Ref<T2>, Ref<T3>, Ref<T4>, Ref<T5>)>.GetEnumerator()
    {
        return GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public struct Enumerator<T1, T2, T3, T4, T5> : IEnumerator<(Ref<T1>, Ref<T2>, Ref<T3>, Ref<T4>, Ref<T5>)>
    where T1 : unmanaged, IComponentData
    where T2 : unmanaged, IComponentData
    where T3 : unmanaged, IComponentData
    where T4 : unmanaged, IComponentData
    where T5 : unmanaged, IComponentData
{
    public int entityIndex;

    public int archetypeIndex;

    public unsafe UnsafePtrList<Archetype>* matchingTypes;

    public bool MoveNext()
    {
        unsafe
        {
            if (matchingTypes->Length == 0)
            {
                return false;
            }

            if (entityIndex + 1 < (*matchingTypes)[archetypeIndex]->EntityCount)
            {
                entityIndex++;
                return true;
            }

            if (archetypeIndex + 1 < (*matchingTypes).Length)
            {
                entityIndex = 0;
                archetypeIndex++;
                return true;
            }

            return false;
        }
    }

    public void Reset()
    {
        entityIndex = default;
    }

    public (Ref<T1>, Ref<T2>, Ref<T3>, Ref<T4>, Ref<T5>) Current
    {
        get
        {
            unsafe
            {
                return (
                    (*matchingTypes)[archetypeIndex]->ResolvComponent<T1>(entityIndex),
                    (*matchingTypes)[archetypeIndex]->ResolvComponent<T2>(entityIndex),
                    (*matchingTypes)[archetypeIndex]->ResolvComponent<T3>(entityIndex),
                    (*matchingTypes)[archetypeIndex]->ResolvComponent<T4>(entityIndex),
                    (*matchingTypes)[archetypeIndex]->ResolvComponent<T5>(entityIndex)
                );
            }
        }
    }

    object IEnumerator.Current => Current;

    public void Dispose()
    {
    }
}

public struct Enumerable<T1, T2, T3, T4, T5, T6> : IEnumerable<(Ref<T1>, Ref<T2>, Ref<T3>, Ref<T4>, Ref<T5>, Ref<T6>)>
    where T1 : unmanaged, IComponentData
    where T2 : unmanaged, IComponentData
    where T3 : unmanaged, IComponentData
    where T4 : unmanaged, IComponentData
    where T5 : unmanaged, IComponentData
    where T6 : unmanaged, IComponentData
{
    public unsafe UnsafePtrList<Archetype>* matchingTypes;

    public Enumerator<T1, T2, T3, T4, T5, T6> GetEnumerator()
    {
        unsafe
        {
            return new Enumerator<T1, T2, T3, T4, T5, T6>()
                { entityIndex = -1, archetypeIndex = 0, matchingTypes = this.matchingTypes };
        }
    }

    IEnumerator<(Ref<T1>, Ref<T2>, Ref<T3>, Ref<T4>, Ref<T5>, Ref<T6>)>
        IEnumerable<(Ref<T1>, Ref<T2>, Ref<T3>, Ref<T4>, Ref<T5>, Ref<T6>)>.GetEnumerator()
    {
        return GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public struct Enumerator<T1, T2, T3, T4, T5, T6> : IEnumerator<(Ref<T1>, Ref<T2>, Ref<T3>, Ref<T4>, Ref<T5>, Ref<T6>)>
    where T1 : unmanaged, IComponentData
    where T2 : unmanaged, IComponentData
    where T3 : unmanaged, IComponentData
    where T4 : unmanaged, IComponentData
    where T5 : unmanaged, IComponentData
    where T6 : unmanaged, IComponentData
{
    public int entityIndex;

    public int archetypeIndex;

    public unsafe UnsafePtrList<Archetype>* matchingTypes;

    public bool MoveNext()
    {
        unsafe
        {
            if (matchingTypes->Length == 0)
            {
                return false;
            }

            if (entityIndex + 1 < (*matchingTypes)[archetypeIndex]->EntityCount)
            {
                entityIndex++;
                return true;
            }

            if (archetypeIndex + 1 < (*matchingTypes).Length)
            {
                entityIndex = 0;
                archetypeIndex++;
                return true;
            }

            return false;
        }
    }

    public void Reset()
    {
        entityIndex = default;
    }

    public (Ref<T1>, Ref<T2>, Ref<T3>, Ref<T4>, Ref<T5>, Ref<T6>) Current
    {
        get
        {
            unsafe
            {
                return (
                    (*matchingTypes)[archetypeIndex]->ResolvComponent<T1>(entityIndex),
                    (*matchingTypes)[archetypeIndex]->ResolvComponent<T2>(entityIndex),
                    (*matchingTypes)[archetypeIndex]->ResolvComponent<T3>(entityIndex),
                    (*matchingTypes)[archetypeIndex]->ResolvComponent<T4>(entityIndex),
                    (*matchingTypes)[archetypeIndex]->ResolvComponent<T5>(entityIndex),
                    (*matchingTypes)[archetypeIndex]->ResolvComponent<T6>(entityIndex)
                );
            }
        }
    }

    object IEnumerator.Current => Current;

    public void Dispose()
    {
    }
}

public struct
    Enumerable<T1, T2, T3, T4, T5, T6, T7> : IEnumerable<(Ref<T1>, Ref<T2>, Ref<T3>, Ref<T4>, Ref<T5>, Ref<T6>, Ref<T7>
    )>
    where T1 : unmanaged, IComponentData
    where T2 : unmanaged, IComponentData
    where T3 : unmanaged, IComponentData
    where T4 : unmanaged, IComponentData
    where T5 : unmanaged, IComponentData
    where T6 : unmanaged, IComponentData
    where T7 : unmanaged, IComponentData
{
    public unsafe UnsafePtrList<Archetype>* matchingTypes;

    public Enumerator<T1, T2, T3, T4, T5, T6, T7> GetEnumerator()
    {
        unsafe
        {
            return new Enumerator<T1, T2, T3, T4, T5, T6, T7>()
                { entityIndex = -1, archetypeIndex = 0, matchingTypes = this.matchingTypes };
        }
    }

    IEnumerator<(Ref<T1>, Ref<T2>, Ref<T3>, Ref<T4>, Ref<T5>, Ref<T6>, Ref<T7>)>
        IEnumerable<(Ref<T1>, Ref<T2>, Ref<T3>, Ref<T4>, Ref<T5>, Ref<T6>, Ref<T7>)>.GetEnumerator()
    {
        return GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public struct
    Enumerator<T1, T2, T3, T4, T5, T6, T7> : IEnumerator<(Ref<T1>, Ref<T2>, Ref<T3>, Ref<T4>, Ref<T5>, Ref<T6>, Ref<T7>
    )>
    where T1 : unmanaged, IComponentData
    where T2 : unmanaged, IComponentData
    where T3 : unmanaged, IComponentData
    where T4 : unmanaged, IComponentData
    where T5 : unmanaged, IComponentData
    where T6 : unmanaged, IComponentData
    where T7 : unmanaged, IComponentData
{
    public int entityIndex;

    public int archetypeIndex;

    public unsafe UnsafePtrList<Archetype>* matchingTypes;

    public bool MoveNext()
    {
        unsafe
        {
            if (matchingTypes->Length == 0)
            {
                return false;
            }

            if (entityIndex + 1 < (*matchingTypes)[archetypeIndex]->EntityCount)
            {
                entityIndex++;
                return true;
            }

            if (archetypeIndex + 1 < (*matchingTypes).Length)
            {
                entityIndex = 0;
                archetypeIndex++;
                return true;
            }

            return false;
        }
    }

    public void Reset()
    {
        entityIndex = default;
    }

    public (Ref<T1>, Ref<T2>, Ref<T3>, Ref<T4>, Ref<T5>, Ref<T6>, Ref<T7>) Current
    {
        get
        {
            unsafe
            {
                return (
                    (*matchingTypes)[archetypeIndex]->ResolvComponent<T1>(entityIndex),
                    (*matchingTypes)[archetypeIndex]->ResolvComponent<T2>(entityIndex),
                    (*matchingTypes)[archetypeIndex]->ResolvComponent<T3>(entityIndex),
                    (*matchingTypes)[archetypeIndex]->ResolvComponent<T4>(entityIndex),
                    (*matchingTypes)[archetypeIndex]->ResolvComponent<T5>(entityIndex),
                    (*matchingTypes)[archetypeIndex]->ResolvComponent<T6>(entityIndex),
                    (*matchingTypes)[archetypeIndex]->ResolvComponent<T7>(entityIndex)
                );
            }
        }
    }

    object IEnumerator.Current => Current;

    public void Dispose()
    {
    }
}

public struct
    Enumerable<T1, T2, T3, T4, T5, T6, T7, T8> : IEnumerable<(Ref<T1>, Ref<T2>, Ref<T3>, Ref<T4>, Ref<T5>, Ref<T6>,
    Ref<T7>, Ref<T8>)>
    where T1 : unmanaged, IComponentData
    where T2 : unmanaged, IComponentData
    where T3 : unmanaged, IComponentData
    where T4 : unmanaged, IComponentData
    where T5 : unmanaged, IComponentData
    where T6 : unmanaged, IComponentData
    where T7 : unmanaged, IComponentData
    where T8 : unmanaged, IComponentData
{
    public unsafe UnsafePtrList<Archetype>* matchingTypes;

    public Enumerator<T1, T2, T3, T4, T5, T6, T7, T8> GetEnumerator()
    {
        unsafe
        {
            return new Enumerator<T1, T2, T3, T4, T5, T6, T7, T8>()
                { entityIndex = -1, archetypeIndex = 0, matchingTypes = this.matchingTypes };
        }
    }

    IEnumerator<(Ref<T1>, Ref<T2>, Ref<T3>, Ref<T4>, Ref<T5>, Ref<T6>, Ref<T7>, Ref<T8>)>
        IEnumerable<(Ref<T1>, Ref<T2>, Ref<T3>, Ref<T4>, Ref<T5>, Ref<T6>, Ref<T7>, Ref<T8>)>.GetEnumerator()
    {
        return GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public struct
    Enumerator<T1, T2, T3, T4, T5, T6, T7, T8> : IEnumerator<(Ref<T1>, Ref<T2>, Ref<T3>, Ref<T4>, Ref<T5>, Ref<T6>,
    Ref<T7>, Ref<T8>)>
    where T1 : unmanaged, IComponentData
    where T2 : unmanaged, IComponentData
    where T3 : unmanaged, IComponentData
    where T4 : unmanaged, IComponentData
    where T5 : unmanaged, IComponentData
    where T6 : unmanaged, IComponentData
    where T7 : unmanaged, IComponentData
    where T8 : unmanaged, IComponentData
{
    public int entityIndex;

    public int archetypeIndex;

    public unsafe UnsafePtrList<Archetype>* matchingTypes;

    public bool MoveNext()
    {
        unsafe
        {
            if (matchingTypes->Length == 0)
            {
                return false;
            }

            if (entityIndex + 1 < (*matchingTypes)[archetypeIndex]->EntityCount)
            {
                entityIndex++;
                return true;
            }

            if (archetypeIndex + 1 < (*matchingTypes).Length)
            {
                entityIndex = 0;
                archetypeIndex++;
                return true;
            }

            return false;
        }
    }

    public void Reset()
    {
        entityIndex = default;
    }

    public (Ref<T1>, Ref<T2>, Ref<T3>, Ref<T4>, Ref<T5>, Ref<T6>, Ref<T7>, Ref<T8>) Current
    {
        get
        {
            unsafe
            {
                return (
                    (*matchingTypes)[archetypeIndex]->ResolvComponent<T1>(entityIndex),
                    (*matchingTypes)[archetypeIndex]->ResolvComponent<T2>(entityIndex),
                    (*matchingTypes)[archetypeIndex]->ResolvComponent<T3>(entityIndex),
                    (*matchingTypes)[archetypeIndex]->ResolvComponent<T4>(entityIndex),
                    (*matchingTypes)[archetypeIndex]->ResolvComponent<T5>(entityIndex),
                    (*matchingTypes)[archetypeIndex]->ResolvComponent<T6>(entityIndex),
                    (*matchingTypes)[archetypeIndex]->ResolvComponent<T7>(entityIndex),
                    (*matchingTypes)[archetypeIndex]->ResolvComponent<T8>(entityIndex)
                );
            }
        }
    }

    object IEnumerator.Current => Current;

    public void Dispose()
    {
    }
}