using System;
using System.Collections.Generic;

namespace TPSS.Data.Models.Entities;

public partial class ProjectDetail
{
    public string ProjectId { get; set; } = null!;

    public string ProjectDetailId { get; set; } = null!;

    public string? ProjectName { get; set; }

    public string? Image { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public DateTime? CreateBy { get; set; }

    public DateTime? UpdateBy { get; set; }

    public string? ProjectDescription { get; set; }

    public bool? Verify { get; set; }

    public virtual Project Project { get; set; } = null!;
}
