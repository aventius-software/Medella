using System.Numerics;

namespace Services.Shared.Data;

public interface IDataService<TModel, TKey> 
    where TModel : class
    where TKey : IBinaryInteger<TKey>
{
    Task<bool> AddModelAsync(TModel model, CancellationToken cancellationToken = default);
    Task<bool> DeleteModelAsync(TKey key, CancellationToken cancellationToken = default);
    Task<TModel?> FindModelAsync(TKey key, CancellationToken cancellationToken = default);
    Task<bool> UpdateModelAsync(TModel model, CancellationToken cancellationToken = default);
}
