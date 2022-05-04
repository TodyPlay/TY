using TY.Entities;
using TY.Worlds;

namespace TY.Systems;

public abstract class SystemBase
{
    private bool _enable = true;

    public bool Enable => _enable;

    public World World;

    public EntityManager EntityManager => World.EntityManager;

    public void Update()
    {
        if (!_enable)
        {
            return;
        }

        try
        {
            OnUpdate();
        }
        catch
        {
            Destroy();
            throw;
        }
    }

    public virtual void Awake()
    {
    }

    public virtual void OnUpdate()
    {
    }

    public void Destroy()
    {
        _enable = false;
    }
}