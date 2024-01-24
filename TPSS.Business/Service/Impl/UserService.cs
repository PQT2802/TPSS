using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPSS.Business.Common;
using TPSS.Data.Models.DTO;
using TPSS.Data.Models.Entities;
using TPSS.Data.Repository;

namespace TPSS.Business.Service.Impl
{
    public class UserService : IUserService
    {
        //DI
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        private string AutoGenerateUserId()
        {
            string latestUserId = _userRepository.GetLatestUserIdAsync().Result;
            // giả sử định dạng user id của bạn là "USxxxxxxx"
            // trích xuất phần số và tăng giá trị lên 1, loại bỏ "US" lấy xxxxxxxx
            int numericpart = int.Parse(latestUserId.Substring(2));
            int newnumericpart = numericpart + 1;

            // tạo ra user id mới
            //us + "xxxxxxxx" | nếu số không đủ thì thay thế = 0 (d8)| 123 => 00000123
            string newuserid = $"US{newnumericpart:d8}";
            return newuserid;
        }

        public async Task<int> CreateUserAsync(UserDTO userDTO)
        {
            try
            {
                User user = new User();               
                user.UserId = AutoGenerateUserId();
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

        //public async Task<int> RegistUserAccount(RegisterDTO registerDTO)
        //{
        //    try
        //    {
        //        if (CheckUserName(registerDTO.Username))
        //        {

        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        //ma hoa password
        public bool CheckUserName(string userName)
        {
            if (userName.Equals(_userRepository.GetUserNameAsync(userName))) { return true; } else { return false; }
        }
        public bool CheckEmail(string email) 
        { 
            if(email.Equals(_userRepository.GetEmailAsync(email))) { return true; } else { return false; }
        }
        public bool CheckPhone(string phone)
        {
            if(phone.Equals(_userRepository.GetPhoneAsync(phone))) { return true; } else { return false;}
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
