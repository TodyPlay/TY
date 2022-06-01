namespace TY.Entities;

public partial class EntitiesForEach
{
    public delegate void V1<T1>(T1 t1);

    public delegate void V2<T1, T2>(T1 t1, T2 t2);

    public delegate void V3<T1, T2, T3>(T1 t1, T2 t2, T3 t3);

    public delegate void V4<T1, T2, T3, T4>(T1 t1, T2 t2, T3 t3, T4 t4);

    public delegate void V5<T1, T2, T3, T4, T5>(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5);

    public delegate void V6<T1, T2, T3, T4, T5, T6>(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6);

    public delegate void V7<T1, T2, T3, T4, T5, T6, T7>(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7);

    public void ForEach<T1>(V1<T1> data)
    {
        var components = EntityManager.FindComponents<T1>();
        foreach (var component in components)
        {
            data.Invoke(component);
        }
    }

    public void ForEach<T1, T2>(V2<T1, T2> data)
    {
        var components = EntityManager.FindComponents<T1, T2>();
        foreach (var component in components)
        {
            data.Invoke(component.Item1, component.Item2);
        }
    }

    public void ForEach<T1, T2, T3>(V3<T1, T2, T3> data)
    {
        var components = EntityManager.FindComponents<T1, T2, T3>();
        foreach (var component in components)
        {
            data.Invoke(component.Item1, component.Item2, component.Item3);
        }
    }

    public void ForEach<T1, T2, T3, T4>(V4<T1, T2, T3, T4> data)
    {
        var components = EntityManager.FindComponents<T1, T2, T3, T4>();
        foreach (var component in components)
        {
            data.Invoke(component.Item1, component.Item2, component.Item3, component.Item4);
        }
    }

    public void ForEach<T1, T2, T3, T4, T5>(V5<T1, T2, T3, T4, T5> data)
    {
        var components = EntityManager.FindComponents<T1, T2, T3, T4, T5>();
        foreach (var component in components)
        {
            data.Invoke(component.Item1, component.Item2, component.Item3, component.Item4, component.Item5);
        }
    }

    public void ForEach<T1, T2, T3, T4, T5, T6>(V6<T1, T2, T3, T4, T5, T6> data)
    {
        var components = EntityManager.FindComponents<T1, T2, T3, T4, T5, T6>();
        foreach (var component in components)
        {
            data.Invoke(component.Item1, component.Item2, component.Item3, component.Item4, component.Item5, component.Item6);
        }
    }

    public void ForEach<T1, T2, T3, T4, T5, T6, T7>(V7<T1, T2, T3, T4, T5, T6, T7> data)
    {
        var components = EntityManager.FindComponents<T1, T2, T3, T4, T5, T6, T7>();
        foreach (var component in components)
        {
            data.Invoke(component.Item1, component.Item2, component.Item3, component.Item4, component.Item5, component.Item6, component.Item7);
        }
    }

}
public partial class EntityManager
{
    public List<T1> FindComponents<T1>()
    {
        return FindComponents(typeof(T1)).Select(v => (T1) v).ToList();
    }

    public List<(T1, T2)> FindComponents<T1, T2>()
    {
        return FindComponents(typeof(T1), typeof(T2)).Select(v => ((T1) v[0], (T2) v[1])).ToList();
    }

    public List<(T1, T2, T3)> FindComponents<T1, T2, T3>()
    {
        return FindComponents(typeof(T1), typeof(T2), typeof(T3)).Select(v => ((T1) v[0], (T2) v[1], (T3) v[2])).ToList();
    }

    public List<(T1, T2, T3, T4)> FindComponents<T1, T2, T3, T4>()
    {
        return FindComponents(typeof(T1), typeof(T2), typeof(T3), typeof(T4)).Select(v => ((T1) v[0], (T2) v[1], (T3) v[2], (T4) v[3])).ToList();
    }

    public List<(T1, T2, T3, T4, T5)> FindComponents<T1, T2, T3, T4, T5>()
    {
        return FindComponents(typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5)).Select(v => ((T1) v[0], (T2) v[1], (T3) v[2], (T4) v[3], (T5) v[4])).ToList();
    }

    public List<(T1, T2, T3, T4, T5, T6)> FindComponents<T1, T2, T3, T4, T5, T6>()
    {
        return FindComponents(typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6)).Select(v => ((T1) v[0], (T2) v[1], (T3) v[2], (T4) v[3], (T5) v[4], (T6) v[5])).ToList();
    }

    public List<(T1, T2, T3, T4, T5, T6, T7)> FindComponents<T1, T2, T3, T4, T5, T6, T7>()
    {
        return FindComponents(typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7)).Select(v => ((T1) v[0], (T2) v[1], (T3) v[2], (T4) v[3], (T5) v[4], (T6) v[5], (T7) v[6])).ToList();
    }

}