using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TPSS.Data.Models.DTO
{
    public class PropertyDTO
    {
        public string? ProjectId { get; set; }

        public string? PropertyTitle { get; set; }

        public double? Price { get; set; }

        public double? Area { get; set; }

        public string? City { get; set; }

        public string? District { get; set; }

        public string? Ward { get; set; }

        public string? Street { get; set; }

        public string? Description { get; set; }
        
        public string? Service { get; set; }

        public IFormFileCollection? Images { get; set; }

    }
}
