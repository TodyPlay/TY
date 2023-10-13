namespace TY.Entities;

public partial class EntityManager
{
    public IEnumerable<T1> Query<T1>()
    {
        return FindComponents(typeof(T1)).Select(v => (T1) v);
    }

    public IEnumerable<(T1, T2)> Query<T1, T2>()
    {
        return FindComponents(typeof(T1), typeof(T2)).Select(v => ((T1) v[0], (T2) v[1]));
    }

    public IEnumerable<(T1, T2, T3)> Query<T1, T2, T3>()
    {
        return FindComponents(typeof(T1), typeof(T2), typeof(T3)).Select(v => ((T1) v[0], (T2) v[1], (T3) v[2]));
    }

    public IEnumerable<(T1, T2, T3, T4)> Query<T1, T2, T3, T4>()
    {
        return FindComponents(typeof(T1), typeof(T2), typeof(T3), typeof(T4)).Select(v => ((T1) v[0], (T2) v[1], (T3) v[2], (T4) v[3]));
    }

    public IEnumerable<(T1, T2, T3, T4, T5)> Query<T1, T2, T3, T4, T5>()
    {
        return FindComponents(typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5)).Select(v => ((T1) v[0], (T2) v[1], (T3) v[2], (T4) v[3], (T5) v[4]));
    }

    public IEnumerable<(T1, T2, T3, T4, T5, T6)> Query<T1, T2, T3, T4, T5, T6>()
    {
        return FindComponents(typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6)).Select(v => ((T1) v[0], (T2) v[1], (T3) v[2], (T4) v[3], (T5) v[4], (T6) v[5]));
    }

    public IEnumerable<(T1, T2, T3, T4, T5, T6, T7)> Query<T1, T2, T3, T4, T5, T6, T7>()
    {
        return FindComponents(typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7)).Select(v => ((T1) v[0], (T2) v[1], (T3) v[2], (T4) v[3], (T5) v[4], (T6) v[5], (T7) v[6]));
    }

}