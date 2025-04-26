using Medella.Shared;
using Microsoft.AspNetCore.Components.WebAssembly.Services;
using MudBlazor.Services;
using Module.Patient.Server.Shared;
using Services.Client.Data;
using Services.Server.Endpoints;

namespace Medella;

/// <summary>
/// For security/accounts with Entra, see https://github.com/dotnet/blazor-samples/tree/main/9.0/BlazorWebAppEntra
/// </summary>
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add MudBlazor services
        builder.Services.AddMudServices();

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents()
            .AddInteractiveWebAssemblyComponents();
        
        // Add module services
        builder.Services.AddPatientModuleServices(builder.Configuration.GetConnectionString("Patient")!);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseWebAssemblyDebugging();
        }
        else
        {
            app.UseExceptionHandler("/Error", createScopeForErrors: true);

            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseAntiforgery();

        app.MapStaticAssets();
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode()
            .AddInteractiveWebAssemblyRenderMode()
            .AddAdditionalAssemblies(typeof(Medella.WebApp.Client._Imports).Assembly);

        app.MapEndpoints();

        app.Run();
    }
}