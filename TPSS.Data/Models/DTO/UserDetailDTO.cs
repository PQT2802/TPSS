using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPSS.Data.Models.Entities;

namespace TPSS.Data.Models.DTO
{
    public class UserDetailDTO
    {
        public string? Phone { get; set; }

        public string? PersonalId { get; set; }

        public string? Avatar { get; set; }

        public string UserId { get; set; } = null!;

        public DateOnly? DateOfBirth { get; set; }

        public string? Address { get; set; }

        public string? Gender { get; set; }

        public DateOnly? CreateDate { get; set; }

        public DateOnly? UpdateDate { get; set; }

        public string? CreateBy { get; set; }

        public string? UpdateBy { get; set; }

        public string? TaxIdentificationNumber { get; set; }

        public string UserDetailId { get; set; } = null!;


    }
}
