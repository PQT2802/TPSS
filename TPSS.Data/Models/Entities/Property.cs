using System;
using System.Collections.Generic;

namespace TPSS.Data.Models.Entities;

public partial class Property
{
    public string PropertyId { get; set; } = null!;

    public string? ProjectId { get; set; }

    public string? PropertyName { get; set; }

    public string? Owner { get; set; }

    public double? Price { get; set; }

    public bool? Verify { get; set; }

    public virtual ICollection<LikeList> LikeLists { get; set; } = new List<LikeList>();

    public virtual User? OwnerNavigation { get; set; }

    public virtual Project? Project { get; set; }

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
