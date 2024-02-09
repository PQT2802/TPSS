using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPSS.Data.Models.DTO
{
    public class ReservationDTO
    {
        public string ReservationId { get; set; } = null!;

        public string? SellerId { get; set; }

        public string? BuyerId { get; set; }

        public string? PropertyId { get; set; }

        public DateTime? BookingDate { get; set; }

        public string? Status { get; set; }

        public int? Priority { get; set; }

        public bool? IsDelete { get; set; }

    }
}
