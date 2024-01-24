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
    public class UserService2 : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService2(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async  Task<int> CreateUserAsync(UserDTO userDTO)
        {
            try
            {
                User user = new User();
                user.UserId = userDTO.UserId;
                user.Username = userDTO.Username;
                user.Email = "1234";
                user.Password = userDTO.Password;
                user.Phone = userDTO.Phone;
                int result = await _userRepository.CreateUserAsync(user);
                return result;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message, e);
            }
        }

        Task<int> IUserService.DeleteUserAsync(string id)
        {
            throw new NotImplementedException();
        }

        Task<User> IUserService.GetUserByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        Task<int> IUserService.UpdateUserAsync(UserDTO user)
        {
            throw new NotImplementedException();
        }
    }
}
