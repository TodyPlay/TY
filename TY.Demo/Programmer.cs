using TY.App;
using TY.Demo.Systems;
using TY.Network.systems;

var application = new Application();

var world = application.CreateWorld();
world.CreateAndGetSystem<NetworkSystem>();
world.CreateAndGetSystem<PlayerSystem>();
world.CreateAndGetSystem<ShowTimeSystem>();

application.Run();