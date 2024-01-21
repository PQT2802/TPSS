using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPSS.Data.Models.DTO
{
    public class ProjectDTO
    {
        public string ProjectId { get; set; } = null!;

        public string? ProjectName { get; set; }

        public string? Status { get; set; }

        public bool? IsDelete { get; set; }
    }
}
