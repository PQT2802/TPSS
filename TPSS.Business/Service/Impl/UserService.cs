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

        public async Task<int> DeleteUserAsync(string id)
        {
            try
            {
                int result = await _userRepository.DeleteUserByIdAsync(id);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }

        }

        public async Task<User> GetUserByIdAsync(string id)
        {
            try
            {                
                User result = await _userRepository.GetUserByIdAsync(id);
                return result;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message, e);
            }
        }

        public async Task<int> UpdateUserAsync(UserDTO userdto)
        {
            try
            {
                User user = new User();
                user.Username = userdto.Username;
                user.Email = userdto.Email;
                user.Password = userdto.Password;
                user.Phone = userdto.Phone;
                int result = await _userRepository.UpdateUserAsync(user);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
    }

}
