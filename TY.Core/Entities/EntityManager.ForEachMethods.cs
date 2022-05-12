namespace TY.Entities;

public partial class EntitiesForEach
{
    public delegate void V1<T0>(T0 t0);

    public delegate void V2<T0, T1>(T0 t0, T1 t1);

    public delegate void V3<T0, T1, T2>(T0 t0, T1 t1, T2 t2);

    public delegate void V4<T0, T1, T2, T3>(T0 t0, T1 t1, T2 t2, T3 t3);

    public delegate void V5<T0, T1, T2, T3, T4>(T0 t0, T1 t1, T2 t2, T3 t3, T4 t4);

    public delegate void V6<T0, T1, T2, T3, T4, T5>(T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5);

    public delegate void V7<T0, T1, T2, T3, T4, T5, T6>(T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6);

    public void ForEach<T0>(V1<T0> data)
    {
        var components = EntityManager.FindComponents<T0>();
        data.Invoke(components);
    }

    public void ForEach<T0, T1>(V2<T0, T1> data)
    {
        var components = EntityManager.FindComponents<T0, T1>();
        data.Invoke((components.Item1, components.Item2));
    }

    public void ForEach<T0, T1, T2>(V3<T0, T1, T2> data)
    {
        var components = EntityManager.FindComponents<T0, T1, T2>();
        data.Invoke((components.Item1, components.Item2, components.Item3));
    }

    public void ForEach<T0, T1, T2, T3>(V4<T0, T1, T2, T3> data)
    {
        var components = EntityManager.FindComponents<T0, T1, T2, T3>();
        data.Invoke((components.Item1, components.Item2, components.Item3, components.Item4));
    }

    public void ForEach<T0, T1, T2, T3, T4>(V5<T0, T1, T2, T3, T4> data)
    {
        var components = EntityManager.FindComponents<T0, T1, T2, T3, T4>();
        data.Invoke((components.Item1, components.Item2, components.Item3, components.Item4, components.Item5));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5>(V6<T0, T1, T2, T3, T4, T5> data)
    {
        var components = EntityManager.FindComponents<T0, T1, T2, T3, T4, T5>();
        data.Invoke((components.Item1, components.Item2, components.Item3, components.Item4, components.Item5,
            components.Item6));
    }

    public void ForEach<T0, T1, T2, T3, T4, T5, T6>(V7<T0, T1, T2, T3, T4, T5, T6> data)
    {
        var components = EntityManager.FindComponents<T0, T1, T2, T3, T4, T5, T6>();
        data.Invoke((components.Item1, components.Item2, components.Item3, components.Item4, components.Item5,
            components.Item6, components.Item7));
    }
}

public partial class EntityManager
{
    public void FindComponents<T0>()
    {
    }
}