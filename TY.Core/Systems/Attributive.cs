namespace TY.Systems;

[AttributeUsage(AttributeTargets.Class)]
public class SystemOrderAttribute : Attribute
{
    public int Order { get; }

    public SystemOrderAttribute(int order)
    {
        Order = order;
    }
}