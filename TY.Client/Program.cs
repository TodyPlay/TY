using System.Text;
using System.Threading.Channels;
using NLog;
using TY.Network.kcp2k.highLevel2;

var logger = LogManager.GetCurrentClassLogger();

var kcpClient = new KcpClient();
kcpClient.OnData += bytes => logger.Debug(Encoding.Default.GetString(bytes));
kcpClient.OnConnected += () => { logger.Debug("连接成功"); };
kcpClient.Connect("127.0.0.1", 9700);

new Thread(() =>
{
    while (true)
    {
        Thread.Sleep(100);
        try
        {
            kcpClient.Update();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}).Start();

while (true)
{
    var readLine = Console.ReadLine();

    if (!string.IsNullOrEmpty(readLine))
    {
        kcpClient.SendData(Encoding.Default.GetBytes(readLine));
    }
}