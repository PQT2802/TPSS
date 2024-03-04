using System;
using System.Collections.Generic;

namespace TPSS.Data.Models.Entities;

public partial class Transaction
{
    public string PaymentId { get; set; } = null!;

    public string TransactionId { get; set; } = null!;

    public double? Amount { get; set; }

    public bool? Status { get; set; }

    public bool? IsDelete { get; set; }

    public virtual Payment Payment { get; set; } = null!;
}
