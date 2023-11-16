using TY.Components;

namespace TY.Entities;

public partial struct EntityManager
{
    public IEnumerable<T1> Query<T1>()
        where T1 : IQueryTypeParameter
    {
        throw new NotImplementedException();
    }

    public IEnumerable<(T1, T2)> Query<T1, T2>()
        where T1 : IQueryTypeParameter
        where T2 : IQueryTypeParameter
    {
        throw new NotImplementedException();
    }

    public IEnumerable<(T1, T2, T3)> Query<T1, T2, T3>()
        where T1 : IQueryTypeParameter
        where T2 : IQueryTypeParameter
        where T3 : IQueryTypeParameter
    {
        throw new NotImplementedException();
    }

    public IEnumerable<(T1, T2, T3, T4)> Query<T1, T2, T3, T4>()
        where T1 : IQueryTypeParameter
        where T2 : IQueryTypeParameter
        where T3 : IQueryTypeParameter
        where T4 : IQueryTypeParameter
    {
        throw new NotImplementedException();
    }

    public IEnumerable<(T1, T2, T3, T4, T5)> Query<T1, T2, T3, T4, T5>()
        where T1 : IQueryTypeParameter
        where T2 : IQueryTypeParameter
        where T3 : IQueryTypeParameter
        where T4 : IQueryTypeParameter
        where T5 : IQueryTypeParameter
    {
        throw new NotImplementedException();
    }

    public IEnumerable<(T1, T2, T3, T4, T5, T6)> Query<T1, T2, T3, T4, T5, T6>()
        where T1 : IQueryTypeParameter
        where T2 : IQueryTypeParameter
        where T3 : IQueryTypeParameter
        where T4 : IQueryTypeParameter
        where T5 : IQueryTypeParameter
        where T6 : IQueryTypeParameter
    {
        throw new NotImplementedException();
    }

    public IEnumerable<(T1, T2, T3, T4, T5, T6, T7)> Query<T1, T2, T3, T4, T5, T6, T7>()
        where T1 : IQueryTypeParameter
        where T2 : IQueryTypeParameter
        where T3 : IQueryTypeParameter
        where T4 : IQueryTypeParameter
        where T5 : IQueryTypeParameter
        where T6 : IQueryTypeParameter
        where T7 : IQueryTypeParameter
    {
        throw new NotImplementedException();
    }
}