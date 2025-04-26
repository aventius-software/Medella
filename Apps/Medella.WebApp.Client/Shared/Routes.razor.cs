using Microsoft.AspNetCore.Components;
using System.Reflection;

namespace Medella.WebApp.Client.Shared;

public class RoutesBase : ComponentBase
{
    protected List<Assembly> LazyLoadedAssemblies = [
        typeof(Module.Patient.Client._Imports).Assembly
    ];
}
