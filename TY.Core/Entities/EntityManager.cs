using TY.Components;
using TY.Worlds;

namespace TY.Entities;

public partial class EntityManager
{
    private uint _currentId;

    private uint NextEntityId => ++_currentId;

    private readonly Entities _entities = new();

    public World World { get; }

    public EntityManager(World world)
    {
        World = world;
    }

    public Entity CreateEntity()
    {
        var entity = new Entity { Index = NextEntityId };

        _entities[entity] = new Components();

        return entity;
    }


    public void AddComponent<T>(Entity entity, T component) where T : class, IComponentData
    {
        if (_entities.TryGetValue(entity, out var components))
        {
            if (!components.ContainsKey(typeof(T)))
            {
                components[typeof(T)] = component;
            }
        }
    }

    public void AddComponent<T>(Entity entity, params T[] components) where T : class, IComponentData
    {
        foreach (var component in components)
        {
            AddComponent(entity, component);
        }
    }

    public List<IComponentData> FindComponents(Type type)
    {
        var result = new List<IComponentData>();

        foreach (var (_, components) in _entities)
        {
            if (components.TryGetValue(type, out var component))
            {
                result.Add(component);
            }
        }

        return result;
    }

    public List<IComponentData[]> FindComponents(params Type[] types)
    {
        var result = new List<IComponentData[]>();

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