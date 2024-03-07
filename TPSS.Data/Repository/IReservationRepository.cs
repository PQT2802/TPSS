using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPSS.Data.Models.Entities;

namespace TPSS.Data.Repository
{
    public interface IReservationRepository
    {
        public Task<int> CreateReservationAsync(Reservation reservation);
        public Task<string> GetColumnData(string columnName, string baseOnData);
        public Task<string> GetLatestReservationIdAsync();
        public Task<IEnumerable<dynamic>> GetReservationForBuyerAsync(string userId);
        public Task<IEnumerable<dynamic>> GetReservationForSellerAsync(string userId,string propertyId);
        public Task<Reservation> GetReservation(string userId, string properetyId);
        public Task<int> DeleteReservation(string reservationId);
        public Task<int> AccpectReservation(string reservationId);
        public Task<int> RejectReservation(string reservationId);

    }
}
