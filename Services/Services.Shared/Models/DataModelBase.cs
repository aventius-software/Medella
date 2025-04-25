using System.Numerics;

namespace Services.Shared.Models;

public abstract class DataModelBase<TKey> where TKey : IBinaryInteger<TKey>
{
    public TKey InternalKey { get; set; } = default!;
    public DateTime DateTimeCreated { get; set; }
    public DateTime? DateTimeUpdated { get; set; }
    public DateTime? DateTimeDeleted { get; set; }
}
