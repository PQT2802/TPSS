using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
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
        private readonly IConfiguration _configuration;


        public UserService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;


        }

<<<<<<< HEAD
        private async Task<string> AutoGenerateUserId()
        {
            string newuserid;
            string latestUserId = await _userRepository.GetLatestUserIdAsync();
            // giả sử định dạng user id của bạn là "USxxxxxxx"
            // trích xuất phần số và tăng giá trị lên 1, loại bỏ "US" lấy xxxxxxxx
            if(latestUserId != null)
            {
                int numericpart = int.Parse(latestUserId[2..]);
                int newnumericpart = numericpart + 1;
                newuserid = $"US{newnumericpart:d8}";
            }
            else
            {
                newuserid = "US00000001";
            }
            // tạo ra user id mới
            //us + "xxxxxxxx" | nếu số không đủ thì thay thế = 0 (d8)| 123 => 00000123
            
            return newuserid;
        }

        public async Task<int> CreateUserAsync(UserDTO userDTO)
        {
            try
            {
                User user = new()
                {
                    UserId = await AutoGenerateUserId(),
                    Username = userDTO.Username,
                    Email = userDTO.Email,
                    Password = userDTO.Password,
                    Phone = userDTO.Phone,
                    IsDelete = false
                };
=======
        public async Task<dynamic> RegistUserAsync(RegisterDTO registerDTO)
        {
            try
            {
                User user = new User();
                //USERNAME//////
                if (!Validator.IsValidUsername(registerDTO.Username))
                {
                    return Result.Failure(RegisterErrors.UsernameIsInvalid(registerDTO.Username));
                    
                }
                else if (await CheckUserName(registerDTO.Username))
                {
                    return Result.Failure(RegisterErrors.UserAlreadyExist(registerDTO.Username));
                }
                else
                {
                    user.Username = registerDTO.Username;
                }
                //PHONE//
                //if(!Validator.IsValidPhone(registerDTO.Phone))
                //{
                //    return Result.Failure(RegisterErrors.PhoneIsInvalid(registerDTO.Phone));
                //}
                //else if (await CheckPhone(registerDTO.Phone))
                //{
                //    return Result.Failure(RegisterErrors.PhoneAlreadyUsed(registerDTO.Phone));
                //}
                //else
                //{
                //    user.Phone = registerDTO.Phone;
                //}
                //Email///
                if (!Validator.IsValidEmail(registerDTO.Email))
                {
                    return Result.Failure(RegisterErrors.EmailIsInvalid(registerDTO.Email));
                }
                else if ( await CheckEmail(registerDTO.Email))
                {
                    return Result.Failure(RegisterErrors.EmailAlreadyUsed(registerDTO.Email));
                }
                else { user.Email = registerDTO.Email;}
                //PASSWORD///
                if (!Validator.IsValidPassword(registerDTO.Password))
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
                user.UserId = await AutoGenerateUserId();
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
                user.UserId = await AutoGenerateUserId();
                user.Username = userDTO.Username;
                user.Email = userDTO.Email;
                user.Password = Encryption.Encrypt(userDTO.Password);
                user.Phone = userDTO.Phone;
                user.RoleId = "R1";
                user.IsDelete = false;
                user.IsDelete = false;
>>>>>>> DEV_THANG
                int result = await _userRepository.CreateUserAsync(user);
                return result;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message, e);
            }
        }


        //public async Task<dynamic> GetUserAccountAsync(LoginDTO loginDTO)
        //{
        //    string column = "";
        //    try
        //    {
        //        if (loginDTO.UsernameOrPhoneOrEmail.Contains("@"))
        //        {
        //            column = "Email";
        //        }
        //        else if (loginDTO.UsernameOrPhoneOrEmail.All(char.IsDigit))
        //        {
        //            column = "Phone";
        //        }
        //        else
        //        {
        //            column = "Username";
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        public async Task<dynamic> GetUserAccountAsync(LoginDTO loginDTO)
        {
            try
            {
                if (!await CheckUserName(loginDTO.Username))
                {
                    return Result.Failure(LoginErrors.UsernameNotExist(loginDTO.Username));
                }
                else
                {
                    User result = await _userRepository.GetUserAccountAsync(loginDTO.Username, Encryption.Encrypt(loginDTO.Password));

                    if (result == null)
                    {
                        return Result.Failure(LoginErrors.PasswordIsWrong);
                    }
                    else if (result.IsDelete == true)
                    {
                        return Result.Failure(LoginErrors.AccountIsDelete);
                    }
                    else
                    {
                        var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, loginDTO.Username),
                    new Claim("UserId", result.UserId),
                    new Claim(ClaimTypes.Email,result.Email),                    
                    new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Role,result.RoleId)

                    
                };

                        var authenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
                        //var authenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("nX9IwCQbu6IEQWVFZijgk8miXIZtZ9PGGQyamYGcyl2Oq1xr5wUgDYBmfkuUPxeMIBE1CnRCE3yZIdFXWgJo4V1frk4dFGup6Nyy"));
                        var token = new JwtSecurityToken(
                            issuer: _configuration["JWT:ValidIssuer"],
                            audience: _configuration["JWT:ValidAudience"],
                            expires: DateTime.UtcNow.AddDays(60),
                            claims : authClaims,
                            signingCredentials: new SigningCredentials(authenKey, SecurityAlgorithms.HmacSha256)
                            ) ;
                        return new JwtSecurityTokenHandler().WriteToken(token);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }



        public async Task<bool> CheckUserName(string userName)
        {
            string existingUsername = await _userRepository.GetUserNameAsync(userName);
            return userName.Equals(existingUsername);
        }
        //public bool CheckEmail(string email) 
        //{ 
        //    if(email.Equals(_userRepository.GetEmailAsync(email))) { return true; } else { return false; }
        //}
        public async Task<bool> CheckEmail(string email)
        {
            string existingEmail = await _userRepository.GetEmailAsync(email);
            return email.Equals(existingEmail);
        }

        //public bool CheckPhone(string phone)
        //{
        //    if(phone.Equals(_userRepository.GetPhoneAsync(phone))) { return true; } else { return false;}
        //}

        public async Task<bool> CheckPhone(string phone)
        {
            string existingPhone = await _userRepository.GetPhoneAsync(phone);
            return phone.Equals(existingPhone);
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
                User user = new()
                {
                    Username = userdto.Username,
                    Email = userdto.Email,
                    Password = userdto.Password,
                    Phone = userdto.Phone
                };
                int result = await _userRepository.UpdateUserAsync(user);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        private async Task<string> AutoGenerateUserId()
        {
            string newuserid = "";
            string latestUserId = await _userRepository.GetLatestUserIdAsync();
            if (latestUserId.IsNullOrEmpty())
            {
                newuserid = "US00000000";
            }
            else
            {
                // giả sử định dạng user id của bạn là "USxxxxxxx"
                // trích xuất phần số và tăng giá trị lên 1, loại bỏ "US" lấy xxxxxxxx
                int numericpart = int.Parse(latestUserId.Substring(2));
                int newnumericpart = numericpart + 1;

                // tạo ra user id mới
                //us + "xxxxxxxx" | nếu số không đủ thì thay thế = 0 (d8)| 123 => 00000123
                newuserid = $"US{newnumericpart:d8}";
            }
            return newuserid;
        }


    }

}
