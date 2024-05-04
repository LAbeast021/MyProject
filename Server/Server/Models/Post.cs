using System;
using System.Collections.Generic;

namespace Server.Models;

public partial class Post
{
    public int PostId { get; set; }

    public int UserId { get; set; }

    public string UserName { get; set; }

    public string Content { get; set; } = null!;

    public string? ImageUrl { get; set; }

    public DateTime CreatedAt { get; set; }

    public int LikesCount { get; set; }

    public virtual ICollection<Like> Likes { get; set; } = new List<Like>();

    public virtual User User { get; set; } = null!;
}
