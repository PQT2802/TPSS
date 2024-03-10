using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPSS.Data.Models.DTO
{
    public class UpdatePropertyDTO
    {
        public PropertyDTO? PropertyDTO { get; set; }
        public string? PropertyId { get; set; }
        public string? PropertyDetailId { get; set; }
        public string? Uid { get; set; }
        public List<string>? URLs { get; set; }
        public IFormFileCollection? Images { get; set; }
    }
}
