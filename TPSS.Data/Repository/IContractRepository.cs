using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPSS.Data.Models.Entities;

namespace TPSS.Data.Repository
{
    public interface IContractRepository
    {
        public Task<int> CreateContractAsync(string contractId, string reservationId);
        public Task<IEnumerable<Contract>> GetContractsByReservationIdAsync(string reservationId);
        public Task<string> GetLatestContractIdAsync();
        public Task<IEnumerable<dynamic>> GetAllContractAsync();
        public Task<IEnumerable<dynamic>> GetAllContractForSellerAsync(string userId);
        public Task<IEnumerable<dynamic>> GetAllContractForBuyerAsync(string userId);
        public Task<int> UpdateContractStatusAsync(string contractId, string status);
        public Task<dynamic> GetContractDetailAsync(string contractId);
        public Task<int> AddContractAsync(string contractId,string contract,string userId);
    }
}
