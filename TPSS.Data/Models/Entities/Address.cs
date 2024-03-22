using System;
using System.Collections.Generic;

namespace TPSS.Data.Models.Entities;

public partial class Address
{
    public string AddressId { get; set; } = null!;

    public string? City { get; set; }

    public virtual ICollection<AddressDetail> AddressDetails { get; set; } = new List<AddressDetail>();
}
