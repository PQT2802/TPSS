using System;
using System.Collections.Generic;

namespace TPSS.Data.Models.Entities;

public partial class Project
{
    public string ProjectId { get; set; } = null!;

    public string? ProjectName { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<Property> Properties { get; set; } = new List<Property>();
}
