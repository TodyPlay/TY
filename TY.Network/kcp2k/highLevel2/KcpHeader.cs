namespace TY.Network.kcp2k.highLevel2;

public enum KcpHeader : byte
{
    Connect = 1,

    Ping,

    Pong,

    Data,

    Disconnect,
}