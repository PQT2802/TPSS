using System;
using System.Collections.Generic;

namespace TPSS.Data.Models.Entities;

public partial class Album
{
    public string ImageId { get; set; } = null!;

    public string? PropertyId { get; set; }

    public string? Image { get; set; }

    public string? ImageDescription { get; set; }

    public virtual Property? Property { get; set; }
}
