using TY.Network.kcp2k.highLevel2;
using TY.Systems;

namespace TY.Network.systems;

public class NetworkSystem : SystemBase
{
    private KcpServer? _kcpServer;

    public event Action<KcpConnection>? OnConnection
    {
        add => _kcpServer!.OnConnection += value;
        remove => _kcpServer!.OnConnection -= value;
    }

    public override void Awake()
    {
        _kcpServer = new KcpServer(9700);
    }

    public override void Start()
    {
        _kcpServer!.Start();
    }

    public override void Update()
    {
        _kcpServer!.Update();
    }

    public override int Order => 0;
}