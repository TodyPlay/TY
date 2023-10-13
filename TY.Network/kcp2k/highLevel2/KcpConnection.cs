using System.Net;
using System.Net.Sockets;
using NLog;
using TY.Network.kcp2k.kcp;

namespace TY.Network.kcp2k.highLevel2;

public class KcpConnection
{
    private Logger _logger = LogManager.GetCurrentClassLogger();
    private EndPoint _endPoint;

    public EndPoint EndPoint => _endPoint;

    private Socket _socket;

    private Kcp _kcp;

    private bool _closed;

    public bool Closed => _closed;

    public event Action<ArraySegment<byte>>? OnData;
    public event Action? OnClosed;
    public event Action? OnPing;
    public event Action? OnPong;

    private byte[]? _buffer;

    public KcpConnection(EndPoint endPoint, Socket socket)
    {
        _socket = socket;
        _endPoint = endPoint;
        _kcp = new Kcp(0, RawSend);
        _buffer = new byte[4 * 1024 * 1024];
    }

    private void RawSend(byte[] data, int size)
    {
        _socket.SendTo(data, 0, size, SocketFlags.None, _endPoint);
    }

    internal void RawInput(byte[] buffer, int offset, int size)
    {
        _kcp.Input(buffer, offset, size);
    }

    internal void Update(uint time)
    {
        _kcp.Update(time);

        var size = _kcp.PeekSize();

        if (size <= 0) return;

        var buffer = new byte[size];
        var len = _kcp.Receive(buffer, buffer.Length);

        if (len > 0)
        {
            switch ((KcpHeader)buffer[0])
            {
                case KcpHeader.Connect:
                    SendConnect();
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
                    _closed = true;
                    OnClosed?.Invoke();
                    break;
            }
        }
    }

    private static readonly byte[] ConnectData = new byte[] { (byte)KcpHeader.Connect };
    private static readonly byte[] PingData = new byte[] { (byte)KcpHeader.Ping };
    private static readonly byte[] PongData = new byte[] { (byte)KcpHeader.Pong };
    private static readonly byte[] DisconnectData = new byte[] { (byte)KcpHeader.Disconnect };

    public void SendConnect()
    {
        Send(ConnectData, ConnectData.Length);
    }

    private void Send(byte[] data, int size)
    {
        _kcp!.Send(data, 0, size);
    }


    public void SendData(byte[] data)
    {
        _buffer![0] = (byte)KcpHeader.Data;
        Buffer.BlockCopy(data, 0, _buffer, 1, data.Length);
        Send(_buffer, data.Length + 1);
    }
}