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
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

         public async Task<int> CreateUserAsync(UserDTO userDTO)
        {
            try
            {
                //User user = new User();
                //user.UserId = userDTO.UserId;//generate
                //user.Username = userDTO.Username;
                //user.Password = userDTO.Password;
                //user.Email = userDTO.Email;
                //user.Phone = userDTO.Phone;
                //user.FirstName = userDTO.FirstName;
                //user.LastName = userDTO.LastName;
                //int result = await _userRepository.CreateUserAsync(user);
                //return result;
                User user = new User();
                user.UserId = userDTO.UserId;
                user.Username = userDTO.Username;
                user.Email = userDTO.Email;
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

        public Task<int> DeleteUserAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateUserAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateUserAsync(UserDTO user)
        {
            throw new NotImplementedException();
        }
    }

}
