using System.Diagnostics;
using System.Net.Sockets;
using NLog;
using TY.Network.kcp2k.kcp;

namespace TY.Network.kcp2k.highLevel2;

public class KcpClient
{
    private Logger _logger = LogManager.GetCurrentClassLogger();

    private Socket? _socket;

    private Kcp? _kcp;

    private Stopwatch _stopwatch = Stopwatch.StartNew();

    private byte[] _buffer = new byte[4 * 1024 * 1024];

    private bool _closed;

    private bool _connected;

    public bool Connected => _connected;

    public bool Closed => _closed;

    public event Action<ArraySegment<byte>>? OnData;
    public event Action? OnConnected;
    public event Action? OnClosed;
    public event Action? OnPing;
    public event Action? OnPong;

    public void Connect(string host, int port)
    {
        if (_socket != null)
        {
            return;
        }

        _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        _socket.Blocking = false;
        _socket.Connect(host, port);
        _kcp = new Kcp(0, RawSend);
        SendConnect();
    }

    private static readonly byte[] ConnectData = new byte[] { (byte)KcpHeader.Connect };
    private static readonly byte[] PingData = new byte[] { (byte)KcpHeader.Ping };
    private static readonly byte[] PongData = new byte[] { (byte)KcpHeader.Pong };
    private static readonly byte[] DisconnectData = new byte[] { (byte)KcpHeader.Disconnect };

    public void SendConnect()
    {
        Send(ConnectData, ConnectData.Length);
    }

    public void SendPing()
    {
        Send(PingData, PingData.Length);
    }

    public void SendPong()
    {
        Send(PongData, PongData.Length);
    }

    public void SendData(byte[] data)
    {
        if (!_connected)
        {
            throw new Exception("尚未连接成功");
        }

        _buffer[0] = (byte)KcpHeader.Data;
        Buffer.BlockCopy(data, 0, _buffer, 1, data.Length);
        Send(_buffer, data.Length + 1);
    }

    public void SendDisconnect()
    {
        Send(DisconnectData, DisconnectData.Length);
    }

    private void Send(byte[] data, int size)
    {
        _kcp!.Send(data, 0, size);
    }

    private void RawSend(byte[] data, int size)
    {
        _socket!.Send(data, 0, size, SocketFlags.None);
    }

    public void Update()
    {
        try
        {
            while (_socket!.ReceiveNonBlocking(_buffer, out var size))
            {
                _kcp!.Input(_buffer, 0, size);
            }
        }
        catch (SocketException e)
        {
            Disconnect();
        }

        _kcp!.Update((uint)_stopwatch.ElapsedMilliseconds);

        var peekSize = _kcp.PeekSize();

        if (peekSize > 0)
        {
            var buffer = new byte[peekSize];
            var len = _kcp.Receive(buffer, buffer.Length);

            if (len > 0)
            {
                switch ((KcpHeader)buffer[0])
                {
                    case KcpHeader.Connect:
                        if (!_connected)
                        {
                            _logger.Debug("连接成功");
                            _connected = true;
                            OnConnected?.Invoke();
                        }

                        break;
                    case KcpHeader.Ping:
                        OnPing?.Invoke();
                        break;
                    case KcpHeader.Pong:
                        OnPong?.Invoke();
                        break;
                    case KcpHeader.Data:
                        OnData?.Invoke(new ArraySegment<byte>(buffer, 1, buffer.Length - 1));
                        break;
                    case KcpHeader.Disconnect:
                        _connected = false;
                        _closed = true;
                        OnClosed?.Invoke();
                        break;
                }
            }
        }
    }

    public void Disconnect()
    {
        _connected = false;
        _closed = true;
    }
}