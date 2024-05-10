namespace BlogPostWebApi.Interfaces.Repositories;

public interface IUnitOfWork
{
    ICommentRepository Comments { get; }
    IUserRepository Users { get;}
    IPostRepository Posts { get; }
}