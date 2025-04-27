using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;

namespace Services.Server.Endpoints;

public static class EndpointExtensions
{
    /// <summary>
    /// Adds endpoints defined using IEndpoint in the specified assembly. Taken from 
    /// https://www.milanjovanovic.tech/blog/automatically-register-minimal-apis-in-aspnetcore
    /// </summary>
    /// <param name="services"></param>
    /// <param name="assembly"></param>
    /// <returns></returns>
    public static IServiceCollection AddEndpoints(this IServiceCollection services, Assembly assembly)
    {
        ServiceDescriptor[] serviceDescriptors = assembly
            .DefinedTypes
            .Where(type => type is { IsAbstract: false, IsInterface: false } && type.IsAssignableTo(typeof(IEndpoint)))
            .Select(type => ServiceDescriptor.Transient(typeof(IEndpoint), type))
            .ToArray();

        services.TryAddEnumerable(serviceDescriptors);

        return services;
    }

    /// <summary>
    /// Maps all previously added endpoints using AddEndpoints. Taken from 
    /// https://www.milanjovanovic.tech/blog/automatically-register-minimal-apis-in-aspnetcore
    /// also see 
    /// https://learn.microsoft.com/en-us/aspnet/core/tutorials/min-web-api?view=aspnetcore-9.0&tabs=visual-studio
    /// </summary>
    /// <param name="app"></param>
    /// <param name="routeGroupBuilder"></param>
    /// <returns></returns>
    public static IApplicationBuilder MapEndpoints(this WebApplication app, RouteGroupBuilder? routeGroupBuilder = null)
    {
        IEnumerable<IEndpoint> endpoints = app.Services.GetRequiredService<IEnumerable<IEndpoint>>();
        IEndpointRouteBuilder builder = routeGroupBuilder is null ? app : routeGroupBuilder;

        foreach (IEndpoint endpoint in endpoints)
        {
            endpoint.MapEndpoint(builder);
        }

        return app;
    }
}
