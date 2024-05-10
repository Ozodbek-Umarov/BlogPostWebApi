using BlogPostWebApi.Entities;
using BlogPostWebApi.Interfaces.Common;
using System.Linq.Expressions;

namespace BlogPostWebApi.Interfaces.Repositories;

public interface ICommentRepository : ICreatable<Comment>, IDeletable<Comment>
{
    Task<IQueryable<Comment>> GetAllAsync(Expression<Func<Comment, bool>> expression);
}