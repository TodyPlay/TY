using TY.Components;
using TY.Worlds;

namespace TY.Entities;

public partial class EntityManager
{
    private World _world;

    private uint _currentId;

    private uint NextEntityId => ++_currentId;

    private Dictionary<Entity, List<IComponent>> _entities = new Dictionary<Entity, List<IComponent>>();

    private bool _destroyed;

    public EntityManager(World world)
    {
        _world = world;
    }

    public Entity CreateEntity()
    {
        Entity entity;
        entity.Id = NextEntityId;

        _entities[entity] = new List<IComponent>();

        return entity;
    }


    public void AddComponent<T>(Entity entity, T component) where T : class, IComponent
    {
        _entities[entity].Add(component);
    }

    public void Destroy()
    {
        if (_destroyed)
        {
            return;
        }
        _destroyed = true;
        _entities.Clear();
        _entities = null;
    }
}