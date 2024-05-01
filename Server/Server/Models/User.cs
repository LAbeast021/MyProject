using System;
using System.Collections.Generic;

namespace Server.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string GoogleId { get; set; } = null!;

    public string? ProfilePicture { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Like> Likes { get; set; } = new List<Like>();

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}
