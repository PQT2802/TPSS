using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPSS.Data.Models.DTO
{
    public class SearchPropertyDTO
    {
        public double? minPrice { get; set; }

        public double? maxPrice { get; set; }

        public string? Province { get; set; }

        public string? City { get; set; }

        public string? District { get; set; }

        public string? Ward { get; set; }

        public string? Street { get; set; }

        public double? Area { get; set; }
    }
}
