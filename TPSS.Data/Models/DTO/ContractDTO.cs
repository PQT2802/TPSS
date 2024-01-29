using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPSS.Data.Models.DTO
{
    public class ContractDTO
    {
        public string ContractId { get; set; } = null!;

        public string? ReservationId { get; set; }

        public DateOnly ContractDate { get; set; }

        public string? ContractTerms { get; set; } // dieu khoan, dieu kien cua hop dong 

        public double? Deposit { get; set; } // tien dat coc 

        public string? ContractStatus { get; set; }

        public bool? IsDelete { get; set; }
    }
}
