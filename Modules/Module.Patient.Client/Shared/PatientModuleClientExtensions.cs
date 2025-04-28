using Microsoft.Extensions.DependencyInjection;
using Module.Patient.Client.Features.Patient;
using Module.Patient.Shared.Features.Patient;
using Services.Shared.Data;

namespace Module.Patient.Client.Shared;

public static class PatientModuleClientExtensions
{
    public static IServiceCollection AddPatientModuleServices(this IServiceCollection services)
    {
        // Register the client version of the patient data service
        return services.AddScoped<IDataService<PatientRecord, long>, PatientDataService>();
    }
}
