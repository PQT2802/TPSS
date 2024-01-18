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
                return 1;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message, e);
            }
        }

        public Task<int> DeleteUser(string id)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateUser(User user)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateUser(UserDTO user)
        {
            throw new NotImplementedException();
        }
    }
}
