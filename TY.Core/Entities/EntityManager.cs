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

    public void AddComponent<T>(Entity entity) where T : IComponentData, new()
    {
        AddComponent(entity, new T());
    }

    public void AddComponent(Entity entity, IComponentData component)
    {
        if (_entities.TryGetValue(entity, out var components))
        {
            if (!components.ContainsKey(component.GetType()))
            {
                components[component.GetType()] = component;
            }
        }
    }

    public void AddComponent(Entity entity, params IComponentData[] components)
    {
        foreach (var component in components)
        {
            AddComponent(entity, component);
        }
    }

    public IEnumerable<IComponentData> FindComponents(Type type)
    {
        return FindComponents(new[] { type }).Select(v => v[0]);
    }

    public IEnumerable<IComponentData[]> FindComponents(params Type[] types)
    {
        return _entities.Values.Where(dics => types.All(type => dics.Keys.Contains(type)))
            .Select(dics => dics.Where(pair => types.Contains(pair.Key)).Select(pair => pair.Value).ToArray());
    }
}