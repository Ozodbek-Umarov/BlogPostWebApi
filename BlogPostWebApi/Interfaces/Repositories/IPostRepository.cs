using BlogPostWebApi.Entities;
using BlogPostWebApi.Interfaces.Common;
using System.Linq.Expressions;

namespace BlogPostWebApi.Interfaces.Repositories;

public interface IPostRepository : ICreatable<Post>, IUpdatable<Post>, IReadable<Post>
{
    Task<IQueryable<Post>> GetByNameAsync(Expression<Func<Post, bool>> expression);
}