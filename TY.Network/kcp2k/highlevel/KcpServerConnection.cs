// server needs to store a separate KcpPeer for each connection.
// as well as remoteEndPoint so we know where to send data to.

using System.Net;

namespace TY.Network.kcp2k.highLevel;

// struct to avoid memory indirection
public struct KcpServerConnection
{
    // peer can't be set from constructor at the moment.
    // because peer callbacks need to know 'connection'.
    // see KcpServer.CreateConnection.
    public KcpPeer? Peer;
    public readonly EndPoint RemoteEndPoint;

    public KcpServerConnection(EndPoint remoteEndPoint)
    {
        Peer = null;
        RemoteEndPoint = remoteEndPoint;
    }
}