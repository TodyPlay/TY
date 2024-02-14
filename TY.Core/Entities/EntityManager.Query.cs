
using TY.Components;
using TY.Unmanaged;

namespace TY.Entities;


public partial struct EntityManager
{


    public Enumerable<T1> Query<T1>()
        where T1 : unmanaged, IComponentData
    {
        unsafe
        {
            ComponentType* types = stackalloc ComponentType[1];
            types[0] = ComponentType.ReadWrite<T1>();
            var entityQuery = Query(types, 1);
            return new Enumerable<T1>() { matchingTypes = entityQuery.matchingTypes };
        }
    }

    public Enumerable<T1, T2> Query<T1, T2>()
        where T1 : unmanaged, IComponentData
        where T2 : unmanaged, IComponentData
    {
        unsafe
        {
            ComponentType* types = stackalloc ComponentType[2];
            types[0] = ComponentType.ReadWrite<T1>();
            types[1] = ComponentType.ReadWrite<T2>();
            var entityQuery = Query(types, 2);
            return new Enumerable<T1, T2>() { matchingTypes = entityQuery.matchingTypes };
        }
    }

    public Enumerable<T1, T2, T3> Query<T1, T2, T3>()
        where T1 : unmanaged, IComponentData
        where T2 : unmanaged, IComponentData
        where T3 : unmanaged, IComponentData
    {
        unsafe
        {
            ComponentType* types = stackalloc ComponentType[3];
            types[0] = ComponentType.ReadWrite<T1>();
            types[1] = ComponentType.ReadWrite<T2>();
            types[2] = ComponentType.ReadWrite<T3>();
            var entityQuery = Query(types, 3);
            return new Enumerable<T1, T2, T3>() { matchingTypes = entityQuery.matchingTypes };
        }
    }

    public Enumerable<T1, T2, T3, T4> Query<T1, T2, T3, T4>()
        where T1 : unmanaged, IComponentData
        where T2 : unmanaged, IComponentData
        where T3 : unmanaged, IComponentData
        where T4 : unmanaged, IComponentData
    {
        unsafe
        {
            ComponentType* types = stackalloc ComponentType[4];
            types[0] = ComponentType.ReadWrite<T1>();
            types[1] = ComponentType.ReadWrite<T2>();
            types[2] = ComponentType.ReadWrite<T3>();
            types[3] = ComponentType.ReadWrite<T4>();
            var entityQuery = Query(types, 4);
            return new Enumerable<T1, T2, T3, T4>() { matchingTypes = entityQuery.matchingTypes };
        }
    }

    public Enumerable<T1, T2, T3, T4, T5> Query<T1, T2, T3, T4, T5>()
        where T1 : unmanaged, IComponentData
        where T2 : unmanaged, IComponentData
        where T3 : unmanaged, IComponentData
        where T4 : unmanaged, IComponentData
        where T5 : unmanaged, IComponentData
    {
        unsafe
        {
            ComponentType* types = stackalloc ComponentType[5];
            types[0] = ComponentType.ReadWrite<T1>();
            types[1] = ComponentType.ReadWrite<T2>();
            types[2] = ComponentType.ReadWrite<T3>();
            types[3] = ComponentType.ReadWrite<T4>();
            types[4] = ComponentType.ReadWrite<T5>();
            var entityQuery = Query(types, 5);
            return new Enumerable<T1, T2, T3, T4, T5>() { matchingTypes = entityQuery.matchingTypes };
        }
    }

    public Enumerable<T1, T2, T3, T4, T5, T6> Query<T1, T2, T3, T4, T5, T6>()
        where T1 : unmanaged, IComponentData
        where T2 : unmanaged, IComponentData
        where T3 : unmanaged, IComponentData
        where T4 : unmanaged, IComponentData
        where T5 : unmanaged, IComponentData
        where T6 : unmanaged, IComponentData
    {
        unsafe
        {
            ComponentType* types = stackalloc ComponentType[6];
            types[0] = ComponentType.ReadWrite<T1>();
            types[1] = ComponentType.ReadWrite<T2>();
            types[2] = ComponentType.ReadWrite<T3>();
            types[3] = ComponentType.ReadWrite<T4>();
            types[4] = ComponentType.ReadWrite<T5>();
            types[5] = ComponentType.ReadWrite<T6>();
            var entityQuery = Query(types, 6);
            return new Enumerable<T1, T2, T3, T4, T5, T6>() { matchingTypes = entityQuery.matchingTypes };
        }
    }

    public Enumerable<T1, T2, T3, T4, T5, T6, T7> Query<T1, T2, T3, T4, T5, T6, T7>()
        where T1 : unmanaged, IComponentData
        where T2 : unmanaged, IComponentData
        where T3 : unmanaged, IComponentData
        where T4 : unmanaged, IComponentData
        where T5 : unmanaged, IComponentData
        where T6 : unmanaged, IComponentData
        where T7 : unmanaged, IComponentData
    {
        unsafe
        {
            ComponentType* types = stackalloc ComponentType[7];
            types[0] = ComponentType.ReadWrite<T1>();
            types[1] = ComponentType.ReadWrite<T2>();
            types[2] = ComponentType.ReadWrite<T3>();
            types[3] = ComponentType.ReadWrite<T4>();
            types[4] = ComponentType.ReadWrite<T5>();
            types[5] = ComponentType.ReadWrite<T6>();
            types[6] = ComponentType.ReadWrite<T7>();
            var entityQuery = Query(types, 7);
            return new Enumerable<T1, T2, T3, T4, T5, T6, T7>() { matchingTypes = entityQuery.matchingTypes };
        }
    }

    public Enumerable<T1, T2, T3, T4, T5, T6, T7, T8> Query<T1, T2, T3, T4, T5, T6, T7, T8>()
        where T1 : unmanaged, IComponentData
        where T2 : unmanaged, IComponentData
        where T3 : unmanaged, IComponentData
        where T4 : unmanaged, IComponentData
        where T5 : unmanaged, IComponentData
        where T6 : unmanaged, IComponentData
        where T7 : unmanaged, IComponentData
        where T8 : unmanaged, IComponentData
    {
        unsafe
        {
            ComponentType* types = stackalloc ComponentType[8];
            types[0] = ComponentType.ReadWrite<T1>();
            types[1] = ComponentType.ReadWrite<T2>();
            types[2] = ComponentType.ReadWrite<T3>();
            types[3] = ComponentType.ReadWrite<T4>();
            types[4] = ComponentType.ReadWrite<T5>();
            types[5] = ComponentType.ReadWrite<T6>();
            types[6] = ComponentType.ReadWrite<T7>();
            types[7] = ComponentType.ReadWrite<T8>();
            var entityQuery = Query(types, 8);
            return new Enumerable<T1, T2, T3, T4, T5, T6, T7, T8>() { matchingTypes = entityQuery.matchingTypes };
        }
    }
    

}