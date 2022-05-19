using System;
using System.Net.Http;
using System.Threading.Tasks;
using TY.Systems;

namespace TY.Demo.Systems;

public class AsyncDemoSystem : SystemBase
{
    protected override async Task OnUpdate()
    {
        var client = new HttpClient();
        await Task.Delay(1000);
        var data = await client.GetStringAsync("https://www.baidu.com");
        Console.WriteLine(data);
    }
}