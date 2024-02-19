using System;
using System.Collections.Generic;

namespace TPSS.Data.Models.Entities;

public partial class Property
{
    public string PropertyId { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public string? PropertyTitle { get; set; }

    public double? Price { get; set; }

    public string? Image { get; set; }

    public double? Area { get; set; }

    public string? Province { get; set; }

    public string? City { get; set; }

    public string? District { get; set; }

    public string? Ward { get; set; }

    public string? Street { get; set; }

    public bool? IsDelete { get; set; }

    public virtual ICollection<LikeList> LikeLists { get; set; } = new List<LikeList>();

    public virtual Project Project { get; set; } = null!;

    public virtual ICollection<PropertyDetail> PropertyDetails { get; set; } = new List<PropertyDetail>();

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

}
