using Asp.Versioning;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Module.Patient.Shared.Features.Patient;
using Services.Server.Endpoints;

namespace Module.Patient.Server.Features.Patient;

public class GetPatient : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var apiVersionSet = app.NewApiVersionSet()
            //.HasDeprecatedApiVersion(new ApiVersion(1, 0))
            //.HasApiVersion(new ApiVersion(2, 0))
            //.HasApiVersion(new ApiVersion(3, 0))
            .HasApiVersion(new ApiVersion(1))
            .ReportApiVersions()
            .Build();

        // https://learn.microsoft.com/en-us/aspnet/core/tutorials/min-web-api?view=aspnetcore-8.0&tabs=visual-studio#add-the-api-code
        app.MapGet("api/patient/{id}", async Task<IResult> (long id, CancellationToken cancellationToken = default) =>
        {
            // await db.Todos.FindAsync(id)
            //   is Todo todo ? Results.Ok(todo) : Results.NotFound());

            return TypedResults.Ok(new PatientRecord
            {
                InternalKey = id,
                HealthAndCareNumber = "example"
            });
        })
            .WithApiVersionSet(apiVersionSet)
            .MapToApiVersion(new ApiVersion(1));
    }
}
