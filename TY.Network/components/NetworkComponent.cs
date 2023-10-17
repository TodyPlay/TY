using System.Text.Json;
using TY.Components;
using TY.Network.kcp2k.highLevel2;

namespace TY.Network.components;

/// <summary>
/// 标识一个网络客户端
/// </summary>
public class NetworkComponent : IComponentData
{
    public KcpConnection? KcpConnection;

    private JsonSerializerOptions _options = new() { IncludeFields = true };

    public void SendData(object data)
    {
        KcpConnection!.SendData(JsonSerializer.SerializeToUtf8Bytes(data, _options));
    }
}