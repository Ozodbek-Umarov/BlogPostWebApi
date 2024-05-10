using BlogPostWebApi.Common.Attributes;
using System.ComponentModel.DataAnnotations;

namespace BlogPostWebApi.DTOs.Posts;

public class AddPostDto
{
    [Required(ErrorMessage = "Title is required")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Post body is required")]
    public string Body { get; set; } = string.Empty;

    [AllowedFileExtensions([".jpg", ".png", ".jpeg"])]
    public List<string>? ImagesPath { get; set; }
}
