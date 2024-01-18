using System;
using System.Collections.Generic;

namespace TPSS.Data.Models.Entities;

public partial class PropertyDetail
{
    public string PropertyDetailId { get; set; } = null!;

    public string? PropertyId { get; set; }

    public string OwnerId { get; set; } = null!;

    public string? PropertyTitle { get; set; }

    public string? Description { get; set; }

    public DateOnly? CreateDate { get; set; }

    public DateOnly? UpdateDate { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public string? Image { get; set; }

    public string? Service { get; set; }

    public string VerifyBy { get; set; } = null!;

    public DateOnly? VerifyDate { get; set; }

    public virtual Property? Property { get; set; }
}
