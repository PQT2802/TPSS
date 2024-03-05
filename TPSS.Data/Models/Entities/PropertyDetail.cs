using System;
using System.Collections.Generic;

namespace TPSS.Data.Models.Entities;

public partial class PropertyDetail
{
    public string PropertyDetailId { get; set; } = null!;

    public string PropertyId { get; set; } = null!;

    public string OwnerId { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? UpdateBy { get; set; }

    public string? Service { get; set; }

    public string? VerifyBy { get; set; }

    public DateTime? VerifyDate { get; set; }

    public bool? Verify { get; set; }

    public string? Status { get; set; }

    public string? CreateBy { get; set; }

    public virtual Property Property { get; set; } = null!;
}
