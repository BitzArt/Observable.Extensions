using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace BitzArt.Observable.Extensions.SampleBlazorApp.Client;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);

        await builder.Build().RunAsync();
    }
}