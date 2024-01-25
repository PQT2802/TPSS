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
    internal class ContractService : IContractService
    {
        public readonly IContractRepository _contractRepository;

        public ContractService(IContractRepository contractRepository)
        {
            _contractRepository = contractRepository;
        }
        public Task<int> CreateContractAsync(ContractDTO user)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteContractAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Contract> GetContractByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateContractAsync(ContractDTO user)
        {
            throw new NotImplementedException();
        }
    }
}
