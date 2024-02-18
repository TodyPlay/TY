using TY.App;
using TY.Demo.Systems;

var application = new Application();

var world = application.CreateWorld("Timing");

world.CreateAndGetSystem<ShowTimeSystem>();

application.Run();


