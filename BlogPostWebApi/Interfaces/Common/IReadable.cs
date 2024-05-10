using System.Linq.Expressions;

namespace BlogPostWebApi.Interfaces.Common;

public interface IReadable<T>
{
    Task<T?> GetByIdAsync(int id);
    Task<IQueryable<T>> GetAllAsync(Expression<Func<T,bool>> expression);
}
