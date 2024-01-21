using System;
using System.Collections.Generic;

namespace TPSS.Data.Models.Entities;

public partial class Contract
{
    public string ContractId { get; set; } = null!;

    public string? ReservationId { get; set; }

    public DateOnly? ContractDate { get; set; }

    public string? ContractTerms { get; set; } // dieu khoan 

    public double? Deposit { get; set; } // tien dat coc 

    public string? ContractStatus { get; set; }

    public bool? IsDelete { get; set; }

    public virtual Reservation? Reservation { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
