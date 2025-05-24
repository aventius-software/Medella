using System.Numerics;

namespace Services.Shared.Models;

public abstract class ReferenceDataModelBase<TKey, TNationalCode> where TKey : IBinaryInteger<TKey>
{
    public TKey InternalKey { get; set; } = default!;
    public DateTime DateTimeCreated { get; set; }
    public DateTime? DateTimeUpdated { get; set; }
    public DateTime? DateTimeDeleted { get; set; }
    public DateOnly ValidFromDate { get; set; }
    public DateOnly ValidToDate { get; set; }
    public TNationalCode NationalCode { get; set; } = default!;
    public string NationalDescription { get; set; } = default!;
    public string ShortDescription { get; set; } = default!;
}
