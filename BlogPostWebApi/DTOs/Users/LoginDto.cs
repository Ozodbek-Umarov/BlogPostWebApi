using BlogPostWebApi.Common.Attributes;
using System.ComponentModel.DataAnnotations;

namespace BlogPostWebApi.DTOs.Users;

public class LoginDto
{
    [Required(ErrorMessage = "Email is required"), EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required, Password]
    public string Password { get; set; } = string.Empty;
}
