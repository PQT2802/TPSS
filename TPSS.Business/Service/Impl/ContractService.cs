

using System.Net;
using System.Net.Http;
using System.Net.WebSockets;
using TPSS.Data.Models.DTO;
using TPSS.Data.Models.Entities;
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
        
        public async Task<Contract> GetContractByIdAsync(string id)
        {
            try
            {
                Contract result = await _contractRepository.GetContractByIdAsync(id);
                return result;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message, e);
            }
        }
        public async Task<DateOnly> GetdateContractByIdAsync(string id) //lay date
        {
            try
            {
                DateOnly result = await _contractRepository.GetDateContractByIdAsync(id);
                return result;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message, e);
            }
        }

        public async Task<int> CreateContractAsync(ContractDTO newContract)
        {
            try
            {
                Contract contract = new()
                {
                    ContractId = await AutoGenerateContractId(),//tu tao mot id moi
                    ReservationId = newContract.ReservationId,//dto contract a contract b property, 
                    ContractDate = newContract.ContractDate,
                    ContractTerms = newContract.ContractTerms,
                    Deposit = newContract.Deposit,
                    ContractStatus = newContract.ContractStatus,//cap nhat trang thai do nguoi ban suat theo tien trinh nguoi mua
                    IsDelete = false
                };
                int result = await _contractRepository.CreateContractAsync(contract);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public static DateOnly GetTodaysDate()
        {
            return DateOnly.FromDateTime(DateTime.Now);
        }

        public async Task<int> UpdateContractAsync(ContractDTO updateContract)
        {
            try
            {
                Contract contract = new()
                {
                    ContractTerms = updateContract.ContractTerms,
                    Deposit = updateContract.Deposit,
                    ContractDate = updateContract.ContractDate,
                    ReservationId = updateContract.ReservationId,
                    ContractStatus = updateContract.ContractStatus
                }; 
                int result = await _contractRepository.UpdateContractAsync(contract);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task<int> DeleteContractAsync(string id)
        {
            try
            {
                int result = await _contractRepository.DeleteContractAsync(id);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        private async Task<string> AutoGenerateContractId()
        {
            string latestContractId = await _contractRepository.GetLatestContractIdAsync();
            int numericpart = int.Parse(latestContractId[2..]);
            int newnumericpart = numericpart + 1;
            string newContractid = $"CO{newnumericpart:d8}";
            return newContractid;
        }

    }
}

