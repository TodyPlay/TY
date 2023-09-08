using System.Text;
using Newtonsoft.Json;
using TY.Components;
using TY.Network.kcp2k.highLevel;

namespace TY.Network.components;

/// <summary>
/// 标识一个网络客户端
/// </summary>
public class NetworkComponent : IComponentData
{
    public int ConnectionId;

    public KcpServer? KcpServer;

    public void SendData(object obj)
    {
        //TODO 序列化反序列化选择
        KcpServer!.Send(ConnectionId, Encoding.Default.GetBytes(JsonConvert.SerializeObject(obj)), KcpChannel.Reliable);
    }

    public void Disconnection()
    {
        KcpServer!.Disconnect(ConnectionId);
    }
}