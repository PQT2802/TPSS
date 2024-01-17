using System;
using System.Collections.Generic;

namespace TPSS.Data.Models.Entities;

public partial class Role
{
    public string UserId { get; set; } = null!;

    public string Role1 { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
