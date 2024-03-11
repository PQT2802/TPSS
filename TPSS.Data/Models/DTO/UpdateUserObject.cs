using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPSS.Data.Models.DTO
{
    public class UpdateUserObject
    {
        public string? UserId { get; set; } 
        public string? Firstname { get; set; } 
        public string? Lastname { get; set; } 
        public string? Email { get; set; } 
        public string? ConfirmCode { get; set; } 
        public string? Phone { get; set; }
        public string? PersonalId { get; set; }
        public string? Avatar { get; set; }
        public string? DateOfBirth { get; set; }
        public string? Address { get; set; }

        public string? Gender { get; set; }
        public string? TaxIdentificationNumber { get; set; }
    }
}
