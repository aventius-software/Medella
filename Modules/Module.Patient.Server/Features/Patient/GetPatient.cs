using Asp.Versioning;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.OpenApi.Models;
using Module.Patient.Shared.Features.Patient;
using Module.Patient.Shared.Routing;
using Services.Server.Endpoints;
using Services.Shared.Data;

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
        app.MapGet(
            $"{ServerRoutes.Patient}/{{id}}",
            async Task<IResult>
            (
                long id,
                [FromServices] IDataService<PatientRecord, long> dataService,
                CancellationToken cancellationToken = default
            ) =>
            {
                // await db.Todos.FindAsync(id)
                //   is Todo todo ? Results.Ok(todo) : Results.NotFound());

                return TypedResults.Ok(new PatientRecord
                {
                    InternalKey = id,
                    CommunityHealthIndexNumber = "hello",
                    HealthAndCareNumber = "example"
                });
            }
        )
        .WithApiVersionSet(apiVersionSet)
        .MapToApiVersion(new ApiVersion(1))
        .WithName("GetPatientById")
        .WithOpenApi(op => new OpenApiOperation(op)
        {
            Summary = "Get Patient By Id",
            Description = "Returns the record for the specified patient",
            Tags = [new() { Name = "Patient Record" }]
        });
    }
}
