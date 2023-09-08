using NLog;
using TY.Worlds;

namespace TY.App;

public class Application
{
    private static readonly Logger Log = LogManager.GetCurrentClassLogger();

    /// <summary>
    /// 是否启动
    /// </summary>
    private bool _enable = true;

    public bool Enable
    {
        get => _enable;
        set
        {
            _enable = value;
            Log.Warn("应用程序禁用");
        }
    }

    /// <summary>
    /// 默认60帧每秒
    /// </summary>
    public int Frame { get; set; } = 60;

    public int Delay => 1000 / Frame;


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