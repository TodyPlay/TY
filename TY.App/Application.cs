using System.Reflection;
using NLog;
using TY.Worlds;

namespace TY.App;

public class Application
{
    private static readonly Logger Log = LogManager.GetCurrentClassLogger();

    public bool Enable { get; set; } = true;

    /// <summary>
    /// 默认60帧
    /// </summary>
    public int Delay => 1000 / Frame;

    public int Frame { get; set; } = 60;

    public WorldManager WorldManager { get; } = new WorldManager();

    public void Run()
    {
        while (Enable)
        {
            Thread.Sleep(Delay);
            WorldManager.Update();
        }
    }
}