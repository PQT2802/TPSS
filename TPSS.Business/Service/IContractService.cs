using System;
using System.Collections.Generic;
using TPSS.Data.Models.DTO;
using TPSS.Data.Models.Entities;

namespace TPSS.Business.Service
{
    public interface IContractService
    {
        public Task<Contract> GetContractByIdAsync(String id);
        public Task<int> CreateContractAsync(ContractDTO user);
        public Task<int> UpdateContractAsync(ContractDTO user);
        public Task<int> DeleteContractAsync(String id);
    }
}
