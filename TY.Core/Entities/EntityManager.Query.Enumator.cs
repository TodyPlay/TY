using System.Collections;
using TY.Collections;
using TY.Components;
using TY.Unmanaged;

namespace TY.Entities;

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