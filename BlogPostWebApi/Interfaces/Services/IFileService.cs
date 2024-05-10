
namespace BlogPostWebApi.Interfaces.Services;

public interface IFileService
{
    Task<string> SaveImageAsync(IFormFile image);
}
