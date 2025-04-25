namespace Services.Shared.Pagination;

public class PagedResponseModel<TModel>
{
    public IEnumerable<TModel> Items { get; set; } = [];
    public int TotalCount { get; set; }
}
