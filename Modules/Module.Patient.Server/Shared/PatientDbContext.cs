using Microsoft.EntityFrameworkCore;
using Module.Patient.Shared.Features.Patient;
using Module.Patient.Shared.Features.Person;

namespace Module.Patient.Server.Shared;

public class PatientDbContext(DbContextOptions<PatientDbContext> options) : DbContext(options)
{
    private const string PatientSchemaName = "Patient";
    //private const string PersonSchemaName = "Person";

    //public DbSet<PatientRecord> PatientRecords { get; set; }
    //public DbSet<PersonRecord> PersonRecords { get; set; }

    public async Task<PatientRecord?> GetPatientRecordByKeyAsync(long key, CancellationToken cancellationToken = default)
    {
        return await Database
            .SqlQuery<PatientRecord?>($"EXECUTE [{PatientSchemaName}].[uspGetRecordByKey] @key = {key}")
            .SingleOrDefaultAsync(cancellationToken);
    }

    /// <summary>
    /// Map entity models to tables/views in the database
    /// </summary>
    /// <param name="modelBuilder"></param>
    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    // Map the patient record model to table
    //    modelBuilder.Entity<PatientRecord>().ToTable("Record", PatientSchemaName);
    //    modelBuilder.Entity<PatientRecord>().HasKey(nameof(PatientRecord.InternalKey));
    //    modelBuilder.Entity<PatientRecord>().Property(nameof(PatientRecord.DateTimeCreated)).ValueGeneratedOnAdd();

    //    // Map the person record model to table
    //    modelBuilder.Entity<PersonRecord>().ToTable("Record", PersonSchemaName);
    //    modelBuilder.Entity<PersonRecord>().HasKey(nameof(PersonRecord.InternalKey));
    //    modelBuilder.Entity<PersonRecord>().Property(nameof(PersonRecord.DateTimeCreated)).ValueGeneratedOnAdd();

    //    // Finally...
    //    base.OnModelCreating(modelBuilder);
    //}
}
