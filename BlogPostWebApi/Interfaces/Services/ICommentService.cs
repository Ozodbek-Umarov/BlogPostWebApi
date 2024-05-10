using BlogPostWebApi.DTOs.Comments;

namespace BlogPostWebApi.Interfaces.Services;

public interface ICommentService
{
    Task CreateAsync(AddCommentDto dto);
    Task<List<CommentDto>> GetAllAsync();
    Task<CommentDto> GetByIdAsync(int id);
    Task DeleteAsync(int id);
}
