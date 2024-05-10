namespace BlogPostWebApi.Interfaces.Common;

public interface IDeletable<T>
{
    Task DeleteAsync(T entity);
}
