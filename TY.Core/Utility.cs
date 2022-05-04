using System.Reflection;

namespace TY;

public static class Utility
{
    public static List<Type> GetTypesFrom<T>(IEnumerable<Assembly> assemblies)
    {
        return GetTypesFrom(typeof(T), assemblies);
    }

    public static List<Type> GetTypesFrom(Type type, IEnumerable<Assembly> assemblies)
    {
        var list = new List<Type>();

        foreach (var assembly in assemblies)
        {
            var types = assembly.GetTypes();
            foreach (var t in types)
            {
                if (t != type && type.IsAssignableFrom(t))
                {
                    list.Add(t);
                }
            }
        }

        return list;
    }
}