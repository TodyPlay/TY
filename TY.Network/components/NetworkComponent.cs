using System.Text;
using Newtonsoft.Json;
using TY.Components;
using TY.Network.kcp2k.highLevel2;

namespace TY.Network.components;

/// <summary>
/// 标识一个网络客户端
/// </summary>
public class NetworkComponent : IComponentData
{
    public KcpConnection? KcpConnection;

    public void SendData(object data)
    {
        KcpConnection!.SendData(Encoding.Default.GetBytes(JsonConvert.SerializeObject(data)));
    }
}