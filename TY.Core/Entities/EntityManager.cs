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
        var entity = new Entity(NextEntityId);

        _entities[entity] = new List<IComponent>();

        return entity;
    }


    public void AddComponent<T>(Entity entity, T component) where T : class, IComponent
    {
        var components = _entities[entity];
        if (!components.Contains(component))
        {
            components.Add(component);
            entity.Version++;
        }
    }

    public void AddComponent<T>(Entity entity, params T[] components) where T : class, IComponent
    {
        foreach (var component in components)
        {
            AddComponent(entity, component);
        }
    }

    public List<IComponent[]> FindComponents(params Type[] types)
    {
        return new List<IComponent[]>();
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