using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPSS.Data.Models.DTO;
using TPSS.Data.Models.Entities;

namespace TPSS.Business.Service
{
    public interface IReservationService
    {
        public Task<Reservation> GetReservationByIdAsync(String id);
        public Task<int> CreateReservationAsync(ReservationDTO newReservation);
        public Task<int> UpdateReservationAsync(ReservationDTO updateReservation);
        public Task<int> DeleteReservationAsync(String id);
        public Task<DateOnly> GetdateReservationByIdAsync(string id);
    }
}
