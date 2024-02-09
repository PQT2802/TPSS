using System;
using System.Collections.Generic;

namespace TPSS.Data.Models.Entities;

public partial class Reservation
{
    public string ReservationId { get; set; } = null!;

    public string? SellerId { get; set; }

    public string? BuyerId { get; set; }

    public string? PropertyId { get; set; }

    public DateTime? BookingDate { get; set; }

    public string? Status { get; set; }

    public int? Priority { get; set; }

    public bool? IsDelete { get; set; }

    public virtual ICollection<Contract> Contracts { get; set; } = new List<Contract>();

    public virtual Property? Property { get; set; }
}
