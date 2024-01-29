using System;
using System.Collections.Generic;

namespace TPSS.Data.Models.Entities;

public partial class Reservation
{
    public string ReservationId { get; set; } = null!;

    public string? SellerId { get; set; }

    public string? BuyerId { get; set; }

    public string? PropertyId { get; set; }
    //dem xem co trung nhieu khong  
    public DateOnly BookingDate { get; set; }

    public bool? Status { get; set; } //dong y hay khong

    public int? Priority { get; set; }//dem so dat de set 

    public bool? IsDelete { get; set; }

    public virtual ICollection<Contract> Contracts { get; set; } = new List<Contract>();//1 reservation tai sao nhieu contract

    public virtual Property? Property { get; set; }
}
