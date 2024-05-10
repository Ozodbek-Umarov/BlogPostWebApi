using System.ComponentModel.DataAnnotations;

namespace BlogPostWebApi.DTOs.Comments;

public class AddCommentDto
{
    [Required(ErrorMessage = "Comment is required")]
    public string Body { get; set; } = string.Empty;
}
