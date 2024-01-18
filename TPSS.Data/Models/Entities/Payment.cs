using System;
using System.Collections.Generic;

namespace TPSS.Data.Models.Entities;

public partial class Payment
{
    public string PaymentId { get; set; } = null!;

    public string TransactionId { get; set; } = null!;

    public double? Amount { get; set; }

    public bool? Status { get; set; }

    public virtual Transaction Transaction { get; set; } = null!;
}
