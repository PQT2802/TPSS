using System;
using System.Collections.Generic;

namespace TPSS.Data.Models.Entities;

public partial class Contract
{
    public string ContractId { get; set; } = null!;

    public string? ReservationId { get; set; }

    public DateTime? SignDate { get; set; }

    public double? Deposit { get; set; }

    public string? ContractStatus { get; set; }

    public bool? IsDelete { get; set; }

    public string? Contract1 { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Reservation? Reservation { get; set; }
}
