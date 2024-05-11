namespace BlogPostWebApi.Common.Utils;

public class PaginationParams
{
    private const int maxPageSize = 50;
    private int pageSize;

    public int PageSize
    {
        get { return pageSize; }
        set { pageSize = (value > maxPageSize) ? maxPageSize : value; } 
    }

    public int PageIndex { get; set; } = 1;

    public PaginationParams(int pageIndex, int pageSize)
    {
        PageIndex = pageIndex;
        PageSize = pageSize;
    }

    public PaginationParams() { }

    public int SkipCount() => (PageIndex - 1) * PageSize;
}