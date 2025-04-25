using Microsoft.AspNetCore.Routing;

namespace Services.Server.Endpoints;

public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}
