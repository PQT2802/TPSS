using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPSS.Data.Models.DTO;
using TPSS.Data.Models.Entities;
using TPSS.Data.Repository;

namespace TPSS.Business.Service.Impl
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        public ReservationService(IReservationRepository reservationRepository) 
        { 
            _reservationRepository = reservationRepository;
        }
        public async Task<int> CreateReservationAsynce(string userId, string propertyId)
        {
			try
			{
                Reservation reservation = new Reservation() {
                    ReservationId = await AutoGenerateReservationId(),
                    BuyerId = userId,
                    PropertyId = propertyId,
                    SellerId = "",
                    BookingDate = DateTime.Now,
                    Status = "Waiting",
                    Priority = 0,
                    IsDelete = false,
                };
                return 0;
			}
			catch (Exception e )
			{

                throw new Exception(e.Message, e);
            }
        }

        private async Task<string> AutoGenerateReservationId()
        {
            string newReservationId = "";
            string latestReservationId = await _reservationRepository.GetLatestReservationIdAsync();
            if (latestReservationId.IsNullOrEmpty())
            {
                newReservationId = "RE00000000";
            }
            else
            {
                // giả sử định dạng user id của bạn là "USxxxxxxx"
                // trích xuất phần số và tăng giá trị lên 1, loại bỏ "US" lấy xxxxxxxx
                int numericpart = int.Parse(latestReservationId.Substring(2));
                int newnumericpart = numericpart + 1;

                // tạo ra user id mới
                //us + "xxxxxxxx" | nếu số không đủ thì thay thế = 0 (d8)| 123 => 00000123
                newReservationId = $"RE{newnumericpart:d8}";
            }
            return newReservationId;
        }
    }
}
