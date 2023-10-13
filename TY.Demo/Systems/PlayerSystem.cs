using System.Numerics;
using System.Text;
using NLog;
using TY.Demo.Components;
using TY.Network.components;
using TY.Network.kcp2k.highLevel2;
using TY.Network.systems;
using TY.Systems;

namespace TY.Demo.Systems;

public class PlayerSystem : SystemBase
{
    private Logger _logger = LogManager.GetCurrentClassLogger();
    private NetworkSystem? _networkSystem;

    public override void Awake()
    {
        _networkSystem = World.CreateAndGetSystem<NetworkSystem>();

        _networkSystem.OnConnection += CreateNewPlayer;
    }

    protected override void OnUpdate()
    {
        Entities.ForEach((PlayerInfo player) => { player.Position += Vector3.One; });

        Entities.ForEach((PlayerInfo playerInfo, NetworkComponent networkComponent) =>
        {
            if (playerInfo.Position.X % 10 == 0)
            {
                networkComponent.SendData(playerInfo);
            }
        });
    }


    private void CreateNewPlayer(KcpConnection kcpConnection)
    {
        var entity = EntityManager.CreateEntity();

        var networkComponent = new NetworkComponent() { KcpConnection = kcpConnection };

        EntityManager.AddComponent(entity,
            new PlayerInfo { Id = 0, Hp = 100, Name = 0.ToString(), Position = new Vector3(0, 0, 0), Power = 100 });
        networkComponent.KcpConnection!.OnData += bytes =>
        {
            _logger.Info("服务端收到数据：" + Encoding.Default.GetString(bytes));
        };
        EntityManager.AddComponent(entity, networkComponent);
    }
}