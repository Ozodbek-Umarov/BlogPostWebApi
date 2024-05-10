using BlogPostWebApi.DbContexts;
using BlogPostWebApi.Interfaces.Repositories;

namespace BlogPostWebApi.Repositories;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    private readonly AppDbContext _context = context;

    public ICommentRepository Comments => new CommentRepository(_context);

    public IUserRepository Users => new UserRepository(_context);

    public IPostRepository Posts => new PostRepository(_context);
}
