using System.ComponentModel.DataAnnotations.Schema;

namespace BlogPostWebApi.Entities;

public sealed class Post : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public int Views { get; set; }
    public int[] Likes { get; set; } = { };
    public List<string>? ImagesPath { get; set; }
    public int UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = null!;
}