using System.Runtime.InteropServices;
using System.Text.Json;
using TY.Components;
using TY.Network.kcp2k.highLevel2;

namespace TY.Network.components;

/// <summary>
/// 标识一个网络客户端
/// </summary>
public struct NetworkComponent : IComponentData
{
    private GCHandle _kcpConnection;

    public KcpConnection KcpConnection
    {
        get => (_kcpConnection.Target as KcpConnection)!;
        set => _kcpConnection = GCHandle.Alloc(value);
    }

    private static JsonSerializerOptions _options = new() { IncludeFields = true };

    public void SendData(object data)
    {
        KcpConnection.SendData(JsonSerializer.SerializeToUtf8Bytes(data, _options));
    }
}