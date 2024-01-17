using System;
using System.Collections.Generic;

namespace TPSS.Data.Models.Entities;

public partial class LikeList
{
    public int LikeId { get; set; }

    public string? UserId { get; set; }

    public string? PropertyId { get; set; }

    public virtual Property? Property { get; set; }

    public virtual User? User { get; set; }
}
