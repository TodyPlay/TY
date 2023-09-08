using System.Text;
using TY.Network.kcp2k.highLevel;

var client = new KcpClient(
    onConnected: () => { Console.WriteLine($"连接成功"); },
    onData: (bytes, channel) => { Console.WriteLine(Encoding.Default.GetString(bytes)); },
    onDisconnected: () => { Console.WriteLine("关闭连接"); },
    onError: (code, message) => { Console.WriteLine($"错误：{code},{message}"); },
    KcpConfig.CreateDefault()
);


client.Connect("127.0.0.1", 9700);

Thread.Sleep(20);

Task.Run(() =>
{
    while (true)
    {
        Thread.Sleep(10);
        client.Tick();
    }
});

while (true)
{
    var readLine = Console.ReadLine();

    if (!string.IsNullOrEmpty(readLine))
    {
        client.Send(Encoding.Default.GetBytes(readLine), KcpChannel.Reliable);
    }


    if (readLine == "exit")
    {
        break;
    }
}