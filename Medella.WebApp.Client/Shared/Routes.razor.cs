#region Namespaces

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.WebAssembly.Services;
using System.Reflection;

#endregion

namespace Medella.WebApp.Client.Shared;

public class RoutesBase : ComponentBase
{
    #region Private constants

    private readonly string PatientModuleName = "Module.Patient.Client.wasm";

    #endregion

    #region Injected services

    [Inject]
    private LazyAssemblyLoader _assemblyLoader { get; set; } = default!;

    [Inject]
    private ILogger<RoutesBase> _logger { get; set; } = default!;

    #endregion

    #region Protected fields

    protected List<Assembly> LazyLoadedAssemblies = [];
    
    #endregion

    #region Override methods

    protected async Task OnNavigateAsync(NavigationContext args)
    {        
        try
        {
            if (args.Path.StartsWith("patient"))
            {
                if (!LazyLoadedAssemblies.Any(x => x.ManifestModule.Name == PatientModuleName.Replace(".wasm", ".dll")))
                {
                    var loadedAssemblies = await _assemblyLoader.LoadAssembliesAsync([PatientModuleName]);
                    LazyLoadedAssemblies.AddRange(loadedAssemblies);
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError("Error: {Message}", ex.Message);
        }
    }

    #endregion
}
