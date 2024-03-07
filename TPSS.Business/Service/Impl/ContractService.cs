using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPSS.Data.Repository;
using TPSS.Data.Repository.Impl;

namespace TPSS.Business.Service.Impl
{
    public class ContractService : IContractService
    {
        private readonly IContractRepository _contractRepository;
        public ContractService(IContractRepository contractRepository)
        {
            _contractRepository = contractRepository;
        }
        public async Task<int> CreateContractAsync(string reservationId)
        {
            try
            {
                var contractId = await AutoGenerateContractId();
                var result = await _contractRepository.CreateContractAsync(contractId, reservationId);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        public Task<IEnumerable<dynamic>> GetAllContractAsync()
        {
            try
            {
                var result = _contractRepository.GetAllContractAsync();
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        public Task<IEnumerable<dynamic>> GetAllContractForSellerAsync(string userId)
        {
            try
            {
                var result = _contractRepository.GetAllContractForSellerAsync(userId);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        public Task<IEnumerable<dynamic>> GetAllContractForBuyerAsync(string userId)
        {
            try
            {
                var result = _contractRepository.GetAllContractForBuyerAsync(userId);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        public Task<int> UpdateContractStatusAsync(string contractId, string status)
        {
            try
            {
                var result = _contractRepository.UpdateContractStatusAsync(contractId, status);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        public Task<dynamic> GetContractDetailAsync(string contractId)
        {
            try
            {
                var result = _contractRepository.GetContractDetailAsync(contractId);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        public Task<int> AddContractAsync(string contractId, string contract, string userId)
        {
            try
            {
                var result = _contractRepository.AddContractAsync(contractId,contract,userId);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        private async Task<string> AutoGenerateContractId()
        {
            string newContractId = "";
            string latestContractId = await _contractRepository.GetLatestContractIdAsync();
            if (latestContractId.IsNullOrEmpty())
            {
                newContractId = "CT00000000";
            }
            else
            {
                int numericpart = int.Parse(latestContractId.Substring(2));
                int newnumericpart = numericpart + 1;
                newContractId = $"CT{newnumericpart:d8}";
            }
            return newContractId;
        }
    }
}
