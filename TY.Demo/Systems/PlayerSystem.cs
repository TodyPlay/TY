using System.Numerics;
using TY.Demo.Components;
using TY.Network.components;
using TY.Network.kcp2k.highLevel;
using TY.Network.systems;
using TY.Systems;

namespace TY.Demo.Systems;

public class PlayerSystem : SystemBase
{
    private NetworkSystem? _networkSystem;

    public override void Awake()
    {
        _networkSystem = World.CreateAndGetSystem<NetworkSystem>();

        _networkSystem.OnConnected += CreateNewPlayer;
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


    private void CreateNewPlayer(int id, KcpServer kcpServer)
    {
        var entity = EntityManager.CreateEntity();
        EntityManager.AddComponent(entity,
            new PlayerInfo { Id = id, Hp = 100, Name = id.ToString(), Position = new Vector3(0, 0, 0), Power = 100 });
        EntityManager.AddComponent(entity, new NetworkComponent { ConnectionId = id, KcpServer = kcpServer });
    }
}