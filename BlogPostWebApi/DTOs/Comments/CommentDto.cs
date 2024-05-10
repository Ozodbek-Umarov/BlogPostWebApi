using System.ComponentModel.DataAnnotations;

namespace BlogPostWebApi.DTOs.Comments;

public class CommentDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Post ID is required")]
    public int PostId { get; set; }

    [Required(ErrorMessage = "User ID is required")]
    public int UserId { get; set; }

    [Required(ErrorMessage = "Comment body is required")]
    public string Body { get; set; } = string.Empty;
}