using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPSS.Data.Models.DTO
{
    public class ReservationDTO
    {
        public string ReservationId { get; set; } = null!;//=10

        public string? SellerId { get; set; }

        public string? BuyerId { get; set; }

        public string? PropertyId { get; set; } //=10
        //dem xem co trung nhieu khong  
        public DateOnly BookingDate { get; set; }

        public bool? Status { get; set; } //dong y hay khong

        public int? Priority { get; set; }//dem so dat de set //10

        public bool? IsDelete { get; set; }
    }
}
