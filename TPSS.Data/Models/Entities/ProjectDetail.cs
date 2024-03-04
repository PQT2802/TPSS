using System;
using System.Collections.Generic;

namespace TPSS.Data.Models.Entities;

public partial class ProjectDetail
{
    public string ProjectId { get; set; } = null!;

    public string ProjectDetailId { get; set; } = null!;

    public string? Images { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public string? ProjectDescription { get; set; }

    public bool? Verify { get; set; }

    public virtual Project Project { get; set; } = null!;
}
