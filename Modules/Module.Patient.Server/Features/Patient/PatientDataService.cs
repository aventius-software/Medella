using Module.Patient.Server.Shared;
using Module.Patient.Shared.Features.Patient;
using Services.Shared.Data;

namespace Module.Patient.Server.Features.Patient;

public class PatientDataService : IDataService<PatientRecord, long>
{
    private readonly PatientDbContext _context;

    public PatientDataService(PatientDbContext context)
    {
        _context = context;
    }

    public async Task<bool> AddModelAsync(PatientRecord model, CancellationToken cancellationToken = default)
    {
        await _context.AddAsync(model, cancellationToken);
        var result = await _context.SaveChangesAsync(cancellationToken);

        return result > 0;
    }

    public Task<bool> DeleteModelAsync(long key, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<PatientRecord?> FindModelAsync(long key, CancellationToken cancellationToken = default)
    {
        return await _context.FindAsync<PatientRecord>(key);
    }

    public async Task<bool> UpdateModelAsync(PatientRecord model, CancellationToken cancellationToken = default)
    {
        _context.Update(model);
        var result = await _context.SaveChangesAsync(cancellationToken);

        return result > 0;
    }
}
