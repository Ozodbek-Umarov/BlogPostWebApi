using BlogPostWebApi.Entities;

namespace BlogPostWebApi.Interfaces.Services;

public interface IAdminService
{
    Task ChangeUserRoleAsync(int id);
    Task DeleteUserAsync(int id);
    Task<IEnumerable<User>> GetAllAdminAsync();
}
