using Asp.Versioning;
using Medella.Shared;
using Module.Patient.Server.Shared;
using MudBlazor.Services;
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

        // Configure API versioning
        builder.Services.AddApiVersioning(options =>
        {
            // Define the different ways of reading the api version            
            options.ApiVersionReader = ApiVersionReader.Combine(
                new QueryStringApiVersionReader("api-version"),
                new HeaderApiVersionReader("x-version"),
                new MediaTypeApiVersionReader("ver"));

            // Assume and use the default version (1) if no version specified
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1);

            // Return supported versions
            options.ReportApiVersions = true;
        });

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

        // Map all the minimal API endpoints we've added
        app.MapEndpoints();

        // Lets go ;-)
        app.Run();
    }
}