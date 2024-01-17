using System;
using System.Collections.Generic;

namespace TPSS.Data.Models.Entities;

public partial class TransactionProcessing
{
    public string ProcessId { get; set; } = null!;

    public string? ContractId { get; set; }

    public string? Status { get; set; }

    public double? CommissionCalculation { get; set; }

    public double? Payment { get; set; }

    public virtual Contract? Contract { get; set; }
}
