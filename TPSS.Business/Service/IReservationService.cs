using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPSS.Data.Models.DTO;

namespace TPSS.Business.Service
{
    public interface IReservationService
    {
        public Task<int> CreateReservationAsynce(string userId, string propertyId);
        public Task<IEnumerable<dynamic>> GetReservationForBuyerAsync(string userId);
        public Task<IEnumerable<dynamic>> GetReservationForSellerAsync(string userId);
    }
}
