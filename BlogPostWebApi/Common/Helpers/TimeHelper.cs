namespace BlogPostWebApi.Common.Helpers;

public static class TimeHelper
{
    public static DateTime GetCurrentTime()
    {
        return DateTime.UtcNow;
    }
}
