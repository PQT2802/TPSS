using System;
using System.Collections.Generic;

namespace TPSS.Data.Models.Entities;

public partial class Role
{
    public string RoleId { get; set; } = null!;

    public string RoleName { get; set; } = null!;

    public virtual ICollection<UserDetail> UserDetails { get; set; } = new List<UserDetail>();
}
