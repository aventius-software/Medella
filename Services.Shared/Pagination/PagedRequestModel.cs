namespace Services.Shared.Pagination;

public class PagedRequestModel
{
    public IEnumerable<CustomQueryStringParameter> CustomParameters { get; set; } = [];
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public string SearchText { get; set; } = string.Empty;
    public bool SortAscending { get; set; }
    public string SortBy { get; set; } = string.Empty;
}
