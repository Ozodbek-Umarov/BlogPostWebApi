using BlogPostWebApi.DTOs.Comments;

namespace BlogPostWebApi.Interfaces.Services;

public interface ICommentService
{
    Task CreateAsync(AddCommentDto dto);
    Task<IEnumerable<CommentDto>> GetAllAsync(int id);
    Task DeleteAsync(int id);
}
