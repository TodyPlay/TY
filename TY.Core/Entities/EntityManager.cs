using TY.Components;

namespace TY.Entities;

public partial class EntityManager
{
    private uint _currentId;

    private uint NextEntityId => ++_currentId;

    private readonly Dictionary<Entity, Dictionary<Type, IComponent>> _entities = new();

    public Entity CreateEntity()
    {
        var entity = new Entity(NextEntityId);

        _entities[entity] = new Dictionary<Type, IComponent>();

        return entity;
    }


    public void AddComponent<T>(Entity entity, T component) where T : class, IComponent
    {
        var components = _entities[entity];
        if (components.ContainsKey(typeof(T)))
        {
            return;
        }

        components[typeof(T)] = component;
        entity.Version++;
    }

    public void AddComponent<T>(Entity entity, params T[] components) where T : class, IComponent
    {
        foreach (var component in components)
        {
            AddComponent(entity, component);
        }
    }

    public List<IComponent> FindComponents(Type type)
    {
        var result = new List<IComponent>();

        foreach (var (_, components) in _entities)
        {
            if (components.ContainsKey(type))
            {
                result.Add(components[type]);
            }
        }

        return result;
    }

    public List<IComponent[]> FindComponents(params Type[] types)
    {
        var result = new List<IComponent[]>();

        foreach (var (_, components) in _entities)
        {
            if (types.All(type => components.ContainsKey(type)))
            {
                result.Add(types.Select(v => components[v]).ToArray());
            }
        }

        return result;
    }
}