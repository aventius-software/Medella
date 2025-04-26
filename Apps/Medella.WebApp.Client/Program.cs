using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Module.Patient.Client.Shared;
using MudBlazor.Services;

namespace Medella.WebApp.Client;

internal class Program
{
    static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);

        // Add general services
        builder.Services.AddMudServices();                    
        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

        // Add module services
        builder.Services.AddPatientModuleServices();

        await builder.Build().RunAsync();
    }
}