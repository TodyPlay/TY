using TY.Components;

namespace TY.Entities;

public partial struct EntityManager
{
    public IEnumerable<T1> Query<T1>()
        where T1 : unmanaged, IQueryTypeParameter
    {
        throw new NotImplementedException();
    }

    public IEnumerable<(T1, T2)> Query<T1, T2>()
        where T1 : unmanaged, IQueryTypeParameter
        where T2 : unmanaged, IQueryTypeParameter
    {
        throw new NotImplementedException();
    }

    public IEnumerable<(T1, T2, T3)> Query<T1, T2, T3>()
        where T1 : unmanaged, IQueryTypeParameter
        where T2 : unmanaged, IQueryTypeParameter
        where T3 : unmanaged, IQueryTypeParameter
    {
        throw new NotImplementedException();
    }

    public IEnumerable<(T1, T2, T3, T4)> Query<T1, T2, T3, T4>()
        where T1 : unmanaged, IQueryTypeParameter
        where T2 : unmanaged, IQueryTypeParameter
        where T3 : unmanaged, IQueryTypeParameter
        where T4 : unmanaged, IQueryTypeParameter
    {
        throw new NotImplementedException();
    }

    public IEnumerable<(T1, T2, T3, T4, T5)> Query<T1, T2, T3, T4, T5>()
        where T1 : unmanaged, IQueryTypeParameter
        where T2 : unmanaged, IQueryTypeParameter
        where T3 : unmanaged, IQueryTypeParameter
        where T4 : unmanaged, IQueryTypeParameter
        where T5 : unmanaged, IQueryTypeParameter
    {
        throw new NotImplementedException();
    }

    public IEnumerable<(T1, T2, T3, T4, T5, T6)> Query<T1, T2, T3, T4, T5, T6>()
        where T1 : unmanaged, IQueryTypeParameter
        where T2 : unmanaged, IQueryTypeParameter
        where T3 : unmanaged, IQueryTypeParameter
        where T4 : unmanaged, IQueryTypeParameter
        where T5 : unmanaged, IQueryTypeParameter
        where T6 : unmanaged, IQueryTypeParameter
    {
        throw new NotImplementedException();
    }

    public IEnumerable<(T1, T2, T3, T4, T5, T6, T7)> Query<T1, T2, T3, T4, T5, T6, T7>()
        where T1 : unmanaged, IQueryTypeParameter
        where T2 : unmanaged, IQueryTypeParameter
        where T3 : unmanaged, IQueryTypeParameter
        where T4 : unmanaged, IQueryTypeParameter
        where T5 : unmanaged, IQueryTypeParameter
        where T6 : unmanaged, IQueryTypeParameter
        where T7 : unmanaged, IQueryTypeParameter
    {
        throw new NotImplementedException();
    }

}