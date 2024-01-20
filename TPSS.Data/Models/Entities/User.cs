using System;
using System.Collections.Generic;

namespace TPSS.Data.Models.Entities;

public partial class User
{
    public string UserId { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public bool? IsDelete { get; set; }

    public virtual ICollection<LikeList> LikeLists { get; set; } = new List<LikeList>();

    public virtual ICollection<UserDetail> UserDetails { get; set; } = new List<UserDetail>();
}
