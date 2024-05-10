namespace BlogPostWebApi.Interfaces.Common;

public interface IUpdatable<T>
{
    Task UpdateAsync(T entity);
}