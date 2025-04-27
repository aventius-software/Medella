using Module.Patient.Shared.Features.Patient;
using Module.Patient.Shared.Routing;
using System.Net.Http.Json;

namespace Module.Patient.Client.Features.Patient;

public class PatientDataService : IPatientDataService
{
    private readonly HttpClient _httpClient;

    public PatientDataService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task AddPatientAsync(PatientRecord patient, CancellationToken cancellationToken = default)
    {
        await _httpClient.PostAsJsonAsync(ServerRoutes.Patient, patient, cancellationToken);
    }

    public async Task<PatientRecord?> FindPatientAsync(long patientKey, CancellationToken cancellationToken = default)
    {
        return await _httpClient.GetFromJsonAsync<PatientRecord>($"{ServerRoutes.Patient}?hash={DateTime.Now}", cancellationToken);
    }

    public async Task UpdatePatientAsync(PatientRecord patient, CancellationToken cancellationToken = default)
    {
        await _httpClient.PutAsJsonAsync($"{ServerRoutes.Patient}/{patient.InternalKey}", patient, cancellationToken);
    }
}
