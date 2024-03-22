using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPSS.Data.Models.DTO
{
    public class ValidationResult
    {
        public bool IsSuccess { get; set; }
        public List<ValidationError> ValidationErrors { get; set; }

        public class ValidationError
        {
            public string FieldName { get; set; }
            public string ErrorMessage { get; set; }
        }
    }
}
