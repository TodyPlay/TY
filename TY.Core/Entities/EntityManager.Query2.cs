using TY.Components;
using TY.Memory;
using TY.Unmanaged;

namespace TY.Entities;

public partial struct EntityManager
{
    public IEnumerable<Ref<T1>> Query<T1>()
        where T1 : unmanaged, IComponentData
    {
       return ArraySegment<Ref<T1>>.Empty;
    }

    public Enumerable<T1, T2> Query<T1, T2>()
        where T1 : unmanaged, IComponentData
        where T2 : unmanaged, IComponentData
    {
        unsafe
        {
            //TODO 使用帧内存避免手动释放
            ComponentType* types = MemoryUtility.AllocZeroed<ComponentType>(2);
            types[0] = ComponentType.ReadWrite<T1>();
            types[2] = ComponentType.ReadWrite<T2>();

            var entityQuery = Query(types, 2);
            return new Enumerable<T1, T2>() { matchingTypes = entityQuery.matchingTypes };
        }
    }

    public IEnumerable<(Ref<T1>, Ref<T2>, Ref<T3>)> Query<T1, T2, T3>()
        where T1 : unmanaged, IComponentData
        where T2 : unmanaged, IComponentData
        where T3 : unmanaged, IComponentData
    {
        throw new NotImplementedException();
    }

    public IEnumerable<(Ref<T1>, Ref<T2>, Ref<T3>, Ref<T4>)> Query<T1, T2, T3, T4>()
        where T1 : unmanaged, IComponentData
        where T2 : unmanaged, IComponentData
        where T3 : unmanaged, IComponentData
        where T4 : unmanaged, IComponentData
    {
        throw new NotImplementedException();
    }

    public IEnumerable<(Ref<T1>, Ref<T2>, Ref<T3>, Ref<T4>, Ref<T5>)> Query<T1, T2, T3, T4, T5>()
        where T1 : unmanaged, IComponentData
        where T2 : unmanaged, IComponentData
        where T3 : unmanaged, IComponentData
        where T4 : unmanaged, IComponentData
        where T5 : unmanaged, IComponentData
    {
        throw new NotImplementedException();
    }

    public IEnumerable<(Ref<T1>, Ref<T2>, Ref<T3>, Ref<T4>, Ref<T5>, Ref<T6>)> Query<T1, T2, T3, T4, T5, T6>()
        where T1 : unmanaged, IComponentData
        where T2 : unmanaged, IComponentData
        where T3 : unmanaged, IComponentData
        where T4 : unmanaged, IComponentData
        where T5 : unmanaged, IComponentData
        where T6 : unmanaged, IComponentData
    {
        throw new NotImplementedException();
    }

    public IEnumerable<(Ref<T1>, Ref<T2>, Ref<T3>, Ref<T4>, Ref<T5>, Ref<T6>, Ref<T7>)> Query<T1, T2, T3, T4, T5, T6,
        T7>()
        where T1 : unmanaged, IComponentData
        where T2 : unmanaged, IComponentData
        where T3 : unmanaged, IComponentData
        where T4 : unmanaged, IComponentData
        where T5 : unmanaged, IComponentData
        where T6 : unmanaged, IComponentData
        where T7 : unmanaged, IComponentData
    {
        throw new NotImplementedException();
    }
}