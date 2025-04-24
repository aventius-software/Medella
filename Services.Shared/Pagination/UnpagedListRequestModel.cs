namespace Services.Shared.Pagination;

public class UnpagedListRequestModel
{
    public int Count { get; set; }
    public IEnumerable<CustomQueryStringParameter> CustomParameters { get; set; } = [];
    public string SearchText { get; set; } = string.Empty;
    public bool SortAscending { get; set; }
    public string SortBy { get; set; } = string.Empty;
    public int StartIndex { get; set; }
}
