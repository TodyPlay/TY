namespace TY.Collections;

public class SharedInstance<T> where T : class, new()
{
    private static T _instance = new T();

    public static T Instance => _instance;
}