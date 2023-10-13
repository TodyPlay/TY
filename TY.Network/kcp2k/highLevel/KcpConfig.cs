// common config struct, instead of passing 10 parameters manually every time.

using TY.Network.kcp2k.kcp;

namespace TY.Network.kcp2k.highLevel;

// [Serializable] to show it in Unity inspector.
// 'class' so we can set defaults easily.
[Serializable]
public class KcpConfig
{
    // socket configuration ////////////////////////////////////////////////
    // DualMode uses both IPv6 and IPv4. not all platforms support it.
    // (Nintendo Switch, etc.)
    public bool DualMode;

    // UDP servers use only one socket.
    // maximize buffer to handle as many connections as possible.
    //
    //   M1 mac pro:
    //     recv buffer default: 786896 (771 KB)
    //     send buffer default:  9216 (9 KB)
    //     max configurable: ~7 MB
    public int RecvBufferSize;
    public int SendBufferSize;

    // kcp configuration ///////////////////////////////////////////////////
    // configurable MTU in case kcp sits on top of other abstractions like
    // encrypted transports, relays, etc.
    public int Mtu;

    // NoDelay is recommended to reduce latency. This also scales better
    // without buffers getting full.
    public bool NoDelay;

    // KCP internal update interval. 100ms is KCP default, but a lower
    // interval is recommended to minimize latency and to scale to more
    // networked entities.
    public uint Interval;

    // KCP fastresend parameter. Faster resend for the cost of higher
    // bandwidth.
    public int FastResend;

    // KCP congestion window heavily limits messages flushed per update.
    // congestion window may actually be broken in kcp:
    // - sending max sized message @ M1 mac flushes 2-3 messages per update
    // - even with super large send/recv window, it requires thousands of
    //   update calls
    // best to leave this disabled, as it may significantly increase latency.
    public bool CongestionWindow;

    // KCP window size can be modified to support higher loads.
    // for example, Mirror Benchmark requires:
    //   128, 128 for 4k monsters
    //   512, 512 for 10k monsters
    //  8192, 8192 for 20k monsters
    public uint SendWindowSize;
    public uint ReceiveWindowSize;

    // timeout in milliseconds
    public int Timeout;

    // maximum retransmission attempts until dead_link
    public uint MaxRetransmits;

    // constructor /////////////////////////////////////////////////////////
    // constructor with defaults for convenience.
    // makes it easy to define "new KcpConfig(DualMode=false)" etc.
    public KcpConfig(
        bool dualMode = true,
        int recvBufferSize = 1024 * 1024 * 7,
        int sendBufferSize = 1024 * 1024 * 7,
        int mtu = Kcp.MTU_DEF,
        bool noDelay = true,
        uint interval = 10,
        int fastResend = 0,
        bool congestionWindow = false,
        uint sendWindowSize = Kcp.WND_SND,
        uint receiveWindowSize = Kcp.WND_RCV,
        int timeout = KcpPeer.DEFAULT_TIMEOUT,
        uint maxRetransmits = Kcp.DEADLINK)
    {
        this.DualMode = dualMode;
        this.RecvBufferSize = recvBufferSize;
        this.SendBufferSize = sendBufferSize;
        this.Mtu = mtu;
        this.NoDelay = noDelay;
        this.Interval = interval;
        this.FastResend = fastResend;
        this.CongestionWindow = congestionWindow;
        this.SendWindowSize = sendWindowSize;
        this.ReceiveWindowSize = receiveWindowSize;
        this.Timeout = timeout;
        this.MaxRetransmits = maxRetransmits;
    }

    public static KcpConfig CreateDefault()
    {
        return new KcpConfig(
            noDelay: true,
            dualMode: false,
            interval: 1,
            timeout: 2000,
            sendWindowSize: Kcp.WND_SND * 1000,
            receiveWindowSize: Kcp.WND_RCV * 1000,
            congestionWindow: false,
            maxRetransmits: Kcp.DEADLINK * 2
        );
    }
}