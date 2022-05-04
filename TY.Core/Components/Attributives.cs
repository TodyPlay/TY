namespace TY.Components;

/**
 * 当前组件所依赖的组件
 */
[AttributeUsage(AttributeTargets.Class)]
public class RequireComponentAttribute : Attribute
{
    private Type _requireType;

    public RequireComponentAttribute(Type requireType)
    {
        this._requireType = requireType;
    }

    public Type RequireType
    {
        get => _requireType;
        set => _requireType = value ?? throw new ArgumentNullException(nameof(value));
    }
}