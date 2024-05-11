namespace BlogPostWebApi.Common.Utils;

public class PaginationMetaData
{
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public bool HasPrevious => CurrentPage > 1; 
    public bool HasNext => CurrentPage < TotalPages;

    public PaginationMetaData(int totalCount, int pageIndex, int pageSize)
    {
        CurrentPage = pageIndex;
        TotalPages = (int)Math.Ceiling((double)totalCount / pageSize);
        PageSize = pageSize;
    }
}