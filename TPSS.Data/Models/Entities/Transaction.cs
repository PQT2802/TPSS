using System;
using System.Collections.Generic;

namespace TPSS.Data.Models.Entities;

public partial class Transaction
{
    public string TransactionId { get; set; } = null!;

    public string? ContractId { get; set; }

    public string? Status { get; set; }

    public double? CommissionCalculation { get; set; }

    public bool? IsDelete { get; set; }

    public virtual Contract? Contract { get; set; }
}
