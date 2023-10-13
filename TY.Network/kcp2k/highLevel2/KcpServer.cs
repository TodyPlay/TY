using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using NLog;

namespace TY.Network.kcp2k.highLevel2;

public class KcpServer
{
    private Logger _logger = LogManager.GetCurrentClassLogger();

    private Socket? _socket;

    private EndPoint? _endPoint;

    private byte[]? _buffer;

    private Dictionary<EndPoint, KcpConnection>? _connections;

    public event Action<KcpConnection>? OnConnection;

    private Stopwatch? _stopwatch;


    public KcpServer(int port)
    {
        _endPoint = new IPEndPoint(IPAddress.Any, port);
    }

    public void Start()
    {
        if (_socket != null)
        {
            return;
        }

        _buffer = new byte[4 * 1024 * 1024];
        _connections = new Dictionary<EndPoint, KcpConnection>();
        _stopwatch = new Stopwatch();

        _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        _socket.Blocking = false;
        _socket.Bind(_endPoint!);
        _stopwatch.Start();
    }

    public void Update()
    {
        while (_socket!.ReceiveFromNonBlocking(_buffer!, out var size, out var ep))
        {
            ProcessMessage(_buffer!, size, ep);
        }

        foreach (var (_, connection) in _connections!)
        {
            connection.Update((uint)_stopwatch!.ElapsedMilliseconds);
        }

        _connections = _connections.Where(pair => !pair.Value.Closed)
            .ToDictionary(pair => pair.Key, pair => pair.Value);
    }

    private void ProcessMessage(byte[] data, int size, EndPoint ep)
    {
        if (!_connections!.TryGetValue(ep, out var connection))
        {
            _logger.Debug("新的连接");
            connection = _connections[ep] = new KcpConnection(ep, _socket!);
            OnConnection?.Invoke(connection);
        }

        connection.RawInput(data, 0, size);
    }
}