using BlogPostWebApi.DTOs.Posts;

namespace BlogPostWebApi.Interfaces.Services;

public interface IPostService
{
    Task CreateAsync(AddPostDto dto);
    Task<IEnumerable<PostDto>> GetAllAsync();
    Task<PostDto> GetByIdAsync(int id);
    Task<IEnumerable<PostDto>> GetByTitleAsync(string title);
    Task UpdateAsync(PostDto dto);
    Task DeleteAsync(int id);
}
