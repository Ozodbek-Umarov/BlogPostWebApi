using BlogPostWebApi.DbContexts;
using BlogPostWebApi.Entities;
using BlogPostWebApi.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BlogPostWebApi.Repositories;
#pragma warning disable
public class CommentRepository(AppDbContext dbContext) : ICommentRepository
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task CreateAsync(Comment entity)
    {
        await _dbContext.Comments.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Comment entity)
    {
        _dbContext.Comments.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IQueryable<Comment>> GetAllAsync(Expression<Func<Comment, bool>> expression)
    {
        var comments  = _dbContext.Comments.Where(expression);
        return comments;
    }

    public async Task<Comment?> GetByIdAsync(int id)
     => await _dbContext.Comments.FirstOrDefaultAsync(x => x.Id == id);
}
