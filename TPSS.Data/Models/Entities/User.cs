using System;
using System.Collections.Generic;

namespace TPSS.Data.Models.Entities;

public partial class User
{
    public string UserId { get; set; } = null!;

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? DigitalSignature { get; set; }

    public bool? Verify { get; set; }

    public virtual ICollection<LikeList> LikeLists { get; set; } = new List<LikeList>();

    public virtual ICollection<Property> Properties { get; set; } = new List<Property>();

    public virtual ICollection<Reservation> ReservationBuyers { get; set; } = new List<Reservation>();

    public virtual ICollection<Reservation> ReservationSellers { get; set; } = new List<Reservation>();

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
