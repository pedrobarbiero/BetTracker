namespace Application.Common;

public class PagedResult<TResponse>
{
    public required IEnumerable<TResponse> Items { get; set; }
    public required bool HasNextPage { get; set; }
    public required uint Page { get; set; }
    public required uint PageSize { get; set; }
}
