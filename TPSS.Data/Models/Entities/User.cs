using System;
using System.Collections.Generic;

namespace TPSS.Data.Models.Entities;

public partial class User
{
    public string UserId { get; set; } = null!;

    public string? RoleId { get; set; }

    public string? Verify { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? Username { get; set; }

    public virtual ICollection<LikeList> LikeLists { get; set; } = new List<LikeList>();

    public virtual ICollection<Reservation> ReservationBuyers { get; set; } = new List<Reservation>();

    public virtual ICollection<Reservation> ReservationSellers { get; set; } = new List<Reservation>();

    public virtual Role? Role { get; set; }

    public virtual UserDetail UserNavigation { get; set; } = null!;
}
