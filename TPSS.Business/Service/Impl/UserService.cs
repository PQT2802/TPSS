using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPSS.Business.Common;
using TPSS.Business.Exceptions.ErrorHandler;
using TPSS.Data.Models.DTO;
using TPSS.Data.Models.Entities;
using TPSS.Data.Repository;

namespace TPSS.Business.Service.Impl
{
    public sealed class UserService : IUserService
    {
        //DI
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<dynamic> RegisterUser(RegisterDTO registerDTO)
        {
            try
            {
                User user = new User();
                //USERNAME//////
                if (Validator.IsValidUsername(registerDTO.Username))
                {
                    return Result.Failure(RegisterErrors.UsernameIsInvalid(registerDTO.Username));
                    
                }
                else if (CheckUserName(registerDTO.Username))
                {
                    return Result.Failure(RegisterErrors.UserAlreadyExist(registerDTO.Username));
                }
                else
                {
                    user.Username = registerDTO.Username;
                }
                //PHONE//
                if(Validator.IsValidPhone(registerDTO.Phone))
                {
                    return Result.Failure(RegisterErrors.PhoneIsInvalid(registerDTO.Phone));
                }
                else if (CheckPhone(registerDTO.Phone))
                {
                    return Result.Failure(RegisterErrors.PhoneAlreadyUsed(registerDTO.Phone));
                }
                else
                {
                    user.Phone = registerDTO.Phone;
                }
                //Email///
                if (Validator.IsValidEmail(registerDTO.Email))
                {
                    return Result.Failure(RegisterErrors.EmailIsInvalid(registerDTO.Email));
                }
                else if (CheckEmail(registerDTO.Email))
                {
                    return Result.Failure(RegisterErrors.EmailAlreadyUsed(registerDTO.Email));
                }
                else { user.Email = registerDTO.Email;}
                //PASSWORD///
                if (Validator.IsValidPassword(registerDTO.Password))
                {
                    return Result.Failure(RegisterErrors.PasswordIsInvalid(registerDTO.Password));
                }
                else if (!registerDTO.Password.Equals(registerDTO.ConfirmPassword))
                {
                    return Result.Failure(RegisterErrors.ConfirmPasswordIsInvalid);
                }
                else
                {
                    user.Password = Encryption.Encrypt(registerDTO.Password);
                }
                user.IsActive = false;
                user.IsDelete = false;
                user.RoleId = "R1";
                int result = await _userRepository.CreateUserAsync(user);
                return result;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<dynamic> CreateUserAsync(UserDTO userDTO)
        {
            try
            {
                User user = new User();            
                if(userDTO.Username.Equals("0"))
                {
                    return Result.Failure(UserErrors.UserAlreadyExist(userDTO.Username));
                }
                user.UserId = AutoGenerateUserId();
                user.Username = userDTO.Username;
                user.Email = userDTO.Email;
                user.Password = Encryption.Encrypt(userDTO.Password);
                user.Phone = userDTO.Phone;
                user.RoleId = "R1";
                user.IsDelete = false;
                user.IsDelete = false;
                int result = await _userRepository.CreateUserAsync(user);
                return result;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message, e);
            }
        }

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


    }

}
