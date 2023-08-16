using TY.Components;

namespace TY.Entities;

public class Entities : Dictionary<Entity, Components>
{
}

public class Components : Dictionary<Type, IComponent>
{
}