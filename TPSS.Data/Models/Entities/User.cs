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

    public virtual ICollection<LikeList> LikeLists { get; set; } = new List<LikeList>();

    public virtual ICollection<Reservation> ReservationBuyers { get; set; } = new List<Reservation>();

    public virtual ICollection<Reservation> ReservationSellers { get; set; } = new List<Reservation>();

    public virtual UserDetail UserNavigation { get; set; } = null!;
}
