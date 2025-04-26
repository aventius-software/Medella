using Microsoft.Extensions.DependencyInjection;
using Module.Patient.Client.Features.Patient;
using Module.Patient.Shared.Features.Patient;

namespace Module.Patient.Client.Shared;

public static class PatientModuleClientExtensions
{
    public static IServiceCollection AddPatientModuleServices(this IServiceCollection services)
    {
        services.AddScoped<IPatientDataService, PatientDataService>();

        return services;
    }
}
