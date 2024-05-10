using BlogPostWebApi.Entities;
using BlogPostWebApi.Interfaces.Common;
using System.Linq.Expressions;

namespace BlogPostWebApi.Interfaces.Repositories;

public interface IUserRepository : ICreatable<User>, IReadable<User>, IUpdatable<User>
{
    Task<User?> GetByEmailAsync(Expression<Func<User,bool>> expression);
    Task<User?> GetByUsernameAsync(Expression<Func<User,bool>> expression);
}