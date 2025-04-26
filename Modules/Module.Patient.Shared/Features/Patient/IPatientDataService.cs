namespace Module.Patient.Shared.Features.Patient;

public interface IPatientDataService
{
    Task AddPatientAsync(PatientRecord patient, CancellationToken cancellationToken = default);
    Task<PatientRecord?> FindPatientAsync(long patientKey, CancellationToken cancellationToken = default);
    Task UpdatePatientAsync(PatientRecord patient, CancellationToken cancellationToken = default);
}
