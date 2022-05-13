namespace TY.Entities;

public partial class EntityManager
{
    private EntitiesForEach? _entitiesForEach;

    public EntitiesForEach EntitiesForEach => _entitiesForEach ??= new EntitiesForEach(this);
}

public delegate void InvalidForEach(object obj);

public partial class EntitiesForEach
{
    private EntityManager EntityManager { get; }

    public EntitiesForEach(EntityManager entityManager)
    {
        EntityManager = entityManager;
    }

    public void ForEach(InvalidForEach invalidForEach)
    {
        throw new NotImplementedException();
    }
}