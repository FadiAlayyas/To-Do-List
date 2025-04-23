using System.Text.Json.Serialization;

public class PaginatedList<T>
{
    [JsonPropertyName("items")]
    public List<T> Items { get; set; }

    [JsonPropertyName("pageIndex")]
    public int PageIndex { get; set; }

    [JsonPropertyName("totalPages")]
    public int TotalPages { get; set; }

    [JsonPropertyName("hasPreviousPage")]
    public bool HasPreviousPage => PageIndex > 1;

    [JsonPropertyName("hasNextPage")]
    public bool HasNextPage => PageIndex < TotalPages;

    [JsonPropertyName("totalItems")]
    public int TotalItems { get; set; }

    public PaginatedList(List<T> items, int pageIndex, int totalPages, int totalItems)
    {
        Items = items;
        PageIndex = pageIndex;
        TotalPages = totalPages;
        TotalItems = totalItems;
    }
}