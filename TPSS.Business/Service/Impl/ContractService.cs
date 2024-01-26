

using TPSS.Data.Models.DTO;
using TPSS.Data.Models.Entities;
using TPSS.Data.Repository;

namespace TPSS.Business.Service.Impl
{
     public class ContractService : IContractService
    {
        public readonly IContractRepository _contractRepository;

        public ContractService(IContractRepository contractRepository)
        {
            _contractRepository = contractRepository;
        }
        
        public async Task<Contract> GetContractByIdAsync(string id)
        {
            try
            {
                User result = await _contractRepository.GetContractByIdAsync(id);
                return result;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message, e);
            }
        }

        public async Task<int> CreateContractAsync(ContractDTO contractDTO)
        {
             try
            {
                Contract contract = new Contract();
                contract.ContractId = AutoGenerateUserId();
                contract.ContractTerms = contractDTO.ContractTerms;
                contract.Transactions = contractDTO.Con;
                contract.Password = contractDTO.Password;
                contract.Phone = contractDTO.Phone;
                int result = await _contractRepository.CreateContractAsync(contract);
                return result;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message, e);
            }
        }

        public async Task<int> UpdateContractAsync(ContractDTO user)
        {
            try
            {
                User user = new User();
                user.Username = userdto.Username;
                user.Email = userdto.Email;
                user.Password = userdto.Password;
                user.Phone = userdto.Phone;
                int result = await _contractRepository.UpdateUserAsync(user);
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
                int result = await _contractRepository.DeleteUserByIdAsync(id);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
         private async string AutoGenerateUserId()
        {
            string latestUserId = _contractRepository.GetLatestUserIdAsync().Result;
            // giả sử định dạng user id của bạn là "USxxxxxxx"
            // trích xuất phần số và tăng giá trị lên 1, loại bỏ "US" lấy xxxxxxxx
            int numericpart = int.Parse(latestUserId.Substring(2));
            int newnumericpart = numericpart + 1;

            // tạo ra user id mới
            //us + "xxxxxxxx" | nếu số không đủ thì thay thế = 0 (d8)| 123 => 00000123
            string newuserid = $"US{newnumericpart:d8}";
            return newuserid;
        }

    }
}
}
