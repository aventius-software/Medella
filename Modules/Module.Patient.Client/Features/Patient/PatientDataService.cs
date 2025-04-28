using Module.Patient.Shared.Features.Patient;
using Module.Patient.Shared.Routing;
using Services.Shared.Data;
using System.Net.Http.Json;

namespace Module.Patient.Client.Features.Patient;

public class PatientDataService : IDataService<PatientRecord, long>
{
    private readonly HttpClient _httpClient;

    public PatientDataService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> AddModelAsync(PatientRecord model, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PostAsJsonAsync(ServerRoutes.Patient, model, cancellationToken);

        return response.IsSuccessStatusCode;
    }

    public Task<bool> DeleteModelAsync(long key, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<PatientRecord?> FindModelAsync(long key, CancellationToken cancellationToken = default)
    {
        return await _httpClient.GetFromJsonAsync<PatientRecord>($"{ServerRoutes.Patient}/{key}?hash={DateTime.Now}", cancellationToken);
    }

    public async Task<bool> UpdateModelAsync(PatientRecord model, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PutAsJsonAsync($"{ServerRoutes.Patient}/{model.InternalKey}", model, cancellationToken);

        return response.IsSuccessStatusCode;
    }
}
