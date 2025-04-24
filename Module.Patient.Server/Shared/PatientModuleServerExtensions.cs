using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Module.Patient.Shared.Patient;
using Module.Patient.Shared.Person;

namespace Module.Patient.Server.Shared;

public static class PatientModuleServerExtensions
{
    public static IServiceCollection AddPatientModuleServices(this IServiceCollection services, string connectionStringName)
    {
        // Add a DbContext to point to the database
        services.AddDbContext<PatientDbContext>((provider, options) =>
        {
            // Set the Sql server connection string 
            // See https://mderriey.com/2021/07/23/new-easy-way-to-use-aad-auth-with-azure-sql/
            // as you need Microsoft.Data.Client >= v3 for Azure AD auth with SQL!
            options.UseSqlServer(connectionStringName);
        }, ServiceLifetime.Transient);

        // Add OData services for this module      
        //mvcBuilder.AddOData(options => options.AddRouteComponents(ODataRoutes.ModulePrefix, ReportingODataEdmModel.GetEdmModel()));

        // Add any model validators here. Note that failure to do so will result in models being
        // validated ONLY on the client side, but not the server side! You should always validate
        // on the client AND when it gets to the server! Might change this to a more dynamic
        // scanning option instead of individually specifying validators...
        services.AddTransient<IValidator<PatientRecord>, PatientValidator>();
        services.AddTransient<IValidator<PersonRecord>, PersonValidator>();

        return services;
    }
}
