using BlogPostWebApi.DTOs.Users;

namespace BlogPostWebApi.Interfaces.Services;

public interface IAccountService
{
    Task RegisterAsync(AddUserDto dto);
    Task<string> LoginAsync(LoginDto dto);
    Task SendCodeAsync(string email);
    Task<bool> CheckCodeAsync(string email, string code);
}
