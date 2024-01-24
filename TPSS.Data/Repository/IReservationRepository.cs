using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPSS.Data.Models.Entities;

namespace TPSS.Data.Repository
{
    internal interface IReservationRepository
    {
        public Task<Reservation> GetReservationByIdAsync(string reservationId);

        public Task<int> CreateReservationAsync(Reservation newReservation);

        public Task<int> UpdateReservationAsync(Reservation updateReservation);

        public Task<int> DeleteReservationAsync(string reservationId);
    }
}
