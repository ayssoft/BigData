namespace BigData.Core.Common;

/// <summary>
/// Paged result for paginated data
/// </summary>
/// <typeparam name="T">Type of items</typeparam>
public class PagedResult<T>
{
    /// <summary>
    /// Current page number
    /// </summary>
    public int PageNumber { get; set; }

    /// <summary>
    /// Number of items per page
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// Total number of items
    /// </summary>
    public long TotalCount { get; set; }

    /// <summary>
    /// Total number of pages
    /// </summary>
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);

    /// <summary>
    /// Has previous page
    /// </summary>
    public bool HasPreviousPage => PageNumber > 1;

    /// <summary>
    /// Has next page
    /// </summary>
    public bool HasNextPage => PageNumber < TotalPages;

    /// <summary>
    /// Items in current page
    /// </summary>
    public IEnumerable<T> Items { get; set; } = new List<T>();

    /// <summary>
    /// Create paged result
    /// </summary>
    public static PagedResult<T> Create(IEnumerable<T> items, long totalCount, int pageNumber, int pageSize)
    {
        return new PagedResult<T>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
    }
}
