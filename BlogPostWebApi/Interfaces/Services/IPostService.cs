using BlogPostWebApi.DTOs.Posts;

namespace BlogPostWebApi.Interfaces.Services;

public interface IPostService
{
    Task CreateAsync(AddPostDto dto);
    Task<List<PostDto>> GetAllAsync();
    Task<PostDto> GetByIdAsync(int id);
    Task<List<PostDto>> GetByTitleAsync(string title);
    Task UpdateAsync(PostDto dto);
    Task DeleteAsync(int id);
}
