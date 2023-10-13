using System.Net;
using System.Net.Sockets;
using NLog;

namespace TY.Network.kcp2k.highLevel2;

public static class SocketExtension
{
    private static Logger _logger = LogManager.GetCurrentClassLogger();

    public static bool ReceiveFromNonBlocking(this Socket socket, byte[] buffer, out int size, out EndPoint ep)
    {
        try
        {
            if (socket.Poll(0, SelectMode.SelectRead))
            {
                ep = new IPEndPoint(IPAddress.Any, 0);
                size = socket.ReceiveFrom(buffer, ref ep);
                return true;
            }
        }
        catch (SocketException e)
        {
            if (e.SocketErrorCode != SocketError.WouldBlock)
            {
                throw;
            }

            _logger.Debug(e);
        }

        size = default;
        ep = default!;
        return false;
    }

    public static bool ReceiveNonBlocking(this Socket socket, byte[] buffer, out int size)
    {
        try
        {
            if (socket.Poll(0, SelectMode.SelectRead))
            {
                size = socket.Receive(buffer);
                return true;
            }
        }
        catch (SocketException e)
        {
            if (e.SocketErrorCode != SocketError.WouldBlock)
            {
                throw;
            }

            _logger.Debug(e);
        }

        size = 0;
        return false;
    }
}