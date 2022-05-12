using System.Reflection;

namespace TY;

public static class Utility
{
    public static List<Type> GetTypesFromAssembly<T>(IEnumerable<Assembly> assemblies)
    {
        return GetTypesFromAssembly(typeof(T), assemblies);
    }

    public static List<Type> GetTypesFromAssembly(Type type, IEnumerable<Assembly> assemblies)
    {
        var result = new List<Type>();

        foreach (var assembly in assemblies)
        {
            var types = assembly.GetTypes();
            foreach (var t in types)
            {
                if (t != type && type.IsAssignableFrom(t))
                {
                    result.Add(t);
                }
            }
        }

        return result;
    }
}