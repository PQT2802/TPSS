using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPSS.Data.Models.Entities;

namespace TPSS.Data.Repository
{
    internal interface IContractRepository
    {
        public Task<Contract> GetContractByIdAsync(string contractId);
        public Task<int> CreateContractAsync(Contract Createcontract);
        public Task<int> UpdateContractAsync(Contract updateContract);
        public Task<int> DeleteContractAsync(string contractId);
    }
}
