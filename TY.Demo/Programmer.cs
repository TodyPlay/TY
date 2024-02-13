using System;
using TY.App;
using TY.Demo.Systems;


unsafe
{
    int* p = default;

    Console.WriteLine(*p);
}
//
// var application = new Application();
//
// var world = application.CreateWorld("Timing");
//
// world.CreateAndGetSystem<ShowTimeSystem>();
//
// application.Run();