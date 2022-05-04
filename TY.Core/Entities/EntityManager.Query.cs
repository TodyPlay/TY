using TY.Components;

namespace TY.Entities;

public partial class EntityManager
{
    public List<T> Query<T>() where T : class, new()
    {
        var type = typeof(T);
        var fieldInfos = type.GetFields();
        var fieldTypes = fieldInfos.Select(f => f.FieldType).ToList();
        var values = _entities.Values;

        var results = new List<T>();
        foreach (var components in values)
        {
            var componentTypes = components.Select(c => c.GetType()).ToList();
            if (ContainsAll(componentTypes, fieldTypes))
            {
                var result = new T();
                foreach (var fieldInfo in fieldInfos)
                {
                    var component = GetComponentFromType(components, fieldInfo.FieldType);
                    fieldInfo.SetValue(result, component);
                }

                results.Add(result);
            }
        }

        return results;
    }

    private static IComponent GetComponentFromType(List<IComponent> components, Type type)
    {
        foreach (var component in components)
        {
            if (component.GetType() == type)
            {
                return component;
            }
        }

        throw new ArgumentException("错误的参数，无法找到对应类型的组件");
    }

    private static bool ContainsAll(List<Type> main, List<Type> contains)
    {
        foreach (var c in contains)
        {
            var contain = false;
            foreach (var m in main)
            {
                if (c == m)
                {
                    contain = true;
                }
            }

            if (!contain)
            {
                return false;
            }
        }

        return true;
    }
}