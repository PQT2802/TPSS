﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPSS.Data.Models.DTO
{
    public class PropertyDTO
    {
        public string PropertyId { get; set; } = null!;

        public string ProjectId { get; set; } = null!;

        public string OwnerID { get; set; } = null!;

        public string? PropertyTitle { get; set; }

        public double? Price { get; set; }

        public string? Image { get; set; }

        public double? Area { get; set; }

        public string? Province { get; set; }

        public string? City { get; set; }

        public string? District { get; set; }

        public string? Ward { get; set; }

        public string? Street { get; set; }

        public string? CreateBy { get; set; }

        public string? UpdateBy { get; set; }

        public bool? IsDelete { get; set; }

        public string? Description { get; set; }

        public string? Images { get; set; }
        
        public string? Service { get; set; }

    }
}
