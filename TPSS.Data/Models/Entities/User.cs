﻿using System;
using System.Collections.Generic;

namespace TPSS.Data.Models.Entities;

public partial class User
{
    public string UserId { get; set; } = null!;

    public string? Email { get; set; }

    public string? Password { get; set; }

    public bool? IsDelete { get; set; }

    public string? RoleId { get; set; }

    public bool? IsActive { get; set; }

    public string? Firstname { get; set; }

    public string? Lastname { get; set; }

    public virtual ICollection<LikeList> LikeLists { get; set; } = new List<LikeList>();

    public virtual Role? Role { get; set; }

    public virtual ICollection<UserDetail> UserDetails { get; set; } = new List<UserDetail>();
}
