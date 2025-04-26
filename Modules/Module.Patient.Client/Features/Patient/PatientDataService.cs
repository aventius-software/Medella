using Module.Patient.Shared.Features.Patient;
using System.Net.Http.Json;

namespace Module.Patient.Client.Features.Patient;

public class PatientDataService : IPatientDataService
{
    private readonly HttpClient _httpClient;
    private readonly string _route = "api/patient";

    public PatientDataService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task AddPatientAsync(PatientRecord patient, CancellationToken cancellationToken = default)
    {
        await _httpClient.PostAsJsonAsync(_route, patient, cancellationToken);
    }

    public async Task<PatientRecord?> FindPatientAsync(long patientKey, CancellationToken cancellationToken = default)
    {
        return await _httpClient.GetFromJsonAsync<PatientRecord>($"{_route}?hash={DateTime.Now}", cancellationToken);
    }

    public async Task UpdatePatientAsync(PatientRecord patient, CancellationToken cancellationToken = default)
    {
        await _httpClient.PutAsJsonAsync($"{_route}/{patient.InternalKey}", patient, cancellationToken);
    }
}
