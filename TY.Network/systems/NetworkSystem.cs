using NLog;
using TY.Network.kcp2k.highLevel;
using TY.Systems;

namespace TY.Network.systems;

public delegate void OnConnected(int id, KcpServer server);

public delegate void OnData(int id, ArraySegment<byte> data, KcpChannel channel);

public delegate void OnDisconnected(int id);

public delegate void OnError(int id, ErrorCode errorCode, string message);

public class NetworkSystem : SystemBase
{
    private readonly Logger _logger = LogManager.GetCurrentClassLogger();

    private KcpServer? _kcpServer;

    
    public event OnConnected? OnConnected;

    public event OnData? OnData;
    public event OnDisconnected? OnDisconnected;
    public event OnError? OnError;

    public override void Awake()
    {
        _kcpServer = new KcpServer(
            onConnected: id => OnConnected?.Invoke(id, _kcpServer!),
            onData: (id, data, channel) => OnData?.Invoke(id, data, channel),
            onDisconnected: id => OnDisconnected?.Invoke(id),
            onError: (id, code, message) => OnError?.Invoke(id, code, message),
            KcpConfig.CreateDefault()
        );

        _kcpServer.Start(9700);
    }

    protected override void OnUpdate()
    {
        _kcpServer!.Tick();
    }

    public override int Order => 0;
}