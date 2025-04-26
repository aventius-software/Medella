using Module.Patient.Server.Shared;
using Module.Patient.Shared.Features.Patient;

namespace Module.Patient.Server.Features.Patient;

public class PatientDataService : IPatientDataService
{
    private readonly PatientDbContext _context;

    public PatientDataService(PatientDbContext context)
    {
        _context = context;
    }

    public async Task AddPatientAsync(PatientRecord patient, CancellationToken cancellationToken = default)
    {
        await _context.AddAsync(patient, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<PatientRecord?> FindPatientAsync(long patientKey, CancellationToken cancellationToken = default)
    {
        return await _context.FindAsync<PatientRecord>(patientKey);
    }

    public async Task UpdatePatientAsync(PatientRecord patient, CancellationToken cancellationToken = default)
    {
        _context.Update(patient);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
