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

    public IEnumerable<IComponentData> FindComponents(Type type)
    {
        return _entities.Values.Where(dics => dics.ContainsKey(type))
            .Select(dics => dics.Where(pair => pair.Key == type).Select(pair => pair.Value).First());
    }

    public IEnumerable<IComponentData[]> FindComponents(params Type[] types)
    {
        return _entities.Values.Where(dics => types.All(type => dics.Keys.Contains(type)))
            .Select(dics => dics.Where(pair => types.Contains(pair.Key)).Select(pair => pair.Value).ToArray());
    }
}