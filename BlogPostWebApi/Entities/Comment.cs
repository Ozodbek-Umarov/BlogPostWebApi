﻿using System.ComponentModel.DataAnnotations.Schema;

namespace BlogPostWebApi.Entities;

public sealed class Comment
{
    public int Id { get; set; }
    public string Body { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public int PostId { get; set; }
    public int UserId { get; set; }


    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = null!;

    [ForeignKey(nameof(PostId))]
    public Post Post { get; set; } = null!;
}