using System;
using System.Collections.Generic;

namespace TPSS.Data.Models.Entities;

public partial class Payment
{
    public string PaymentId { get; set; } = null!;

    public string? ContractId { get; set; }

    public string? Status { get; set; }

    public double? CommissionCalculation { get; set; }

    public bool? IsDelete { get; set; }

    public double? Amount { get; set; }

    public virtual Contract? Contract { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
