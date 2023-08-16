using NLog;
using TY.Worlds;

namespace TY.App;

public class Application
{
    private static readonly Logger Log = LogManager.GetCurrentClassLogger();

    public bool Enable { get; set; } = true;


    public int Delay => 1000 / Frame;

    /// <summary>
    /// 默认60帧每秒
    /// </summary>
    public int Frame { get; set; } = 60;

    public WorldManager WorldManager { get; } = new();

    public void Run()
    {
        while (Enable)
        {
            Thread.Sleep(Delay);
            WorldManager.Update();
        }
    }
}