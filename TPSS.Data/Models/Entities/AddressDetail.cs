using System;
using System.Collections.Generic;

namespace TPSS.Data.Models.Entities;

public partial class AddressDetail
{
    public string AddressDetailId { get; set; } = null!;

    public string? AddressId { get; set; }

    public string? District { get; set; }

    public string? Ward { get; set; }

    public virtual Address? Address { get; set; }
}
