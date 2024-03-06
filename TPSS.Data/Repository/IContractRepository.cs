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
    }
}
