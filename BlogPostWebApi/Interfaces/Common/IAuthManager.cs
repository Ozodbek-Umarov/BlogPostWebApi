using BlogPostWebApi.Entities;

namespace BlogPostWebApi.Interfaces.Common;

public interface IAuthManager
{
    string GeneratedToken(User user);
}