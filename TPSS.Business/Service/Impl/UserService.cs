using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
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
        private readonly IUserDetailRepository _userDetailRepository;


        public UserService(IUserRepository userRepository, IConfiguration configuration, IUserDetailRepository userDetailRepository )
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _userDetailRepository = userDetailRepository;

        }


        public async Task<dynamic> RegistUserAsync(RegisterDTO registerDTO)
        {
            try
            {

                List<Error> errors = new List<Error>();
                User user = new User();
                bool isValidName = true;

                if (!string.IsNullOrEmpty(registerDTO.Firstname))
                {
                    // Check empty
                    errors.Add(null);
                    if (!Validator.IsValidName(registerDTO.Firstname))
                    {
                        errors.Add(RegisterErrors.UsernameIsInvalid(registerDTO.Firstname));
                        isValidName = false;
                    }
                    else
                    {
                        errors.Add(null);
                    }
                }
                else
                {
                    errors.Add(RegisterErrors.FirstNameIsEmpty);
                    errors.Add(null);
                    isValidName = false;
                }

                if (!string.IsNullOrEmpty(registerDTO.Lastname))
                {
                    // Check empty
                    errors.Add(null);
                    if (!Validator.IsValidUsername(registerDTO.Lastname))
                    {
                        errors.Add(RegisterErrors.UsernameIsInvalid(registerDTO.Lastname));
                        isValidName = false;
                    }
                    else
                    {
                        // Check valid
                        errors.Add(null);
                    }
                }
                else
                {
                    errors.Add(RegisterErrors.LastNameIsEmpty);
                    errors.Add(null);
                    isValidName = false;
                }

                errors.Add(null);
                errors.Add(null);
                // Email validation
                if (!string.IsNullOrEmpty(registerDTO.Email))
                {
                    errors.Add(null);
                    if (!Validator.IsValidEmail(registerDTO.Email))
                    {
                        errors.Add(RegisterErrors.EmailIsInvalid(registerDTO.Email));
                    }
                    else
                    {
                        errors.Add(null);
                    }

                    if (await CheckEmailExistAsync(registerDTO.Email))
                    {
                        errors.Add(RegisterErrors.EmailAlreadyUsed(registerDTO.Email));
                    }
                    else
                    {
                        errors.Add(null);
                    }
                }
                else
                {
                    errors.Add(RegisterErrors.EmailIsEmpty);
                    errors.Add(null);
                    errors.Add(null);
                }

                // Password validation
                if (!string.IsNullOrEmpty(registerDTO.Password))
                {
                    errors.Add(null);
                    if (!Validator.IsValidPassword(registerDTO.Password))
                    {
                        errors.Add(RegisterErrors.PasswordIsInvalid(registerDTO.Password));
                    }
                    else
                    {
                        errors.Add(null);
                    }
                }
                else
                {
                    errors.Add(RegisterErrors.PasswordIsEmpty); // Add the error for passwordIsEmpty only if password is empty
                    errors.Add(null);
                }

                // Confirm password validation
                if (!string.IsNullOrEmpty(registerDTO.ConfirmPassword))
                {
                    errors.Add(null);
                    if (!registerDTO.Password.Equals(registerDTO.ConfirmPassword))
                    {
                        errors.Add(RegisterErrors.ConfirmPasswordIsInvalid);
                    }
                    else
                    {
                        errors.Add(null);
                    }
                }
                else
                {
                    errors.Add(RegisterErrors.ConfirmPasswordIsEmpty);
                    errors.Add(null);
                }

                if (errors.All(error => error == null))
                {
                    user.UserId = await AutoGenerateUserId();
                    user.Firstname = registerDTO.Firstname;
                    user.Lastname = registerDTO.Lastname;
                    user.Email = registerDTO.Email;
                    user.Password = Encryption.Encrypt(registerDTO.Password);
                    user.IsActive = false;
                    user.IsDelete = false;
                    user.RoleId = "R1";
                    int result = await _userRepository.CreateUserAsync(user);
                    if (result == 1)
                    {
                        UserDetail userDetail = new UserDetail();
                        userDetail.UserId = user.UserId;
                        userDetail.UserDetailId = await AutoGenerateUserDetailId();
                        int result2 = await _userDetailRepository.CreateUserDetailAsync(userDetail);
                    }
                    return result;
                }
                return Result.Failures(errors);
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
                //if(userDTO.Username.Equals("0"))
                //{
                //    return Result.Failure(UserErrors.UserAlreadyExist(userDTO.Username));
                //}
                //user.UserId = await AutoGenerateUserId();
                //user.Username = userDTO.Username;
                //user.Email = userDTO.Email;
                //user.Password = Encryption.Encrypt(userDTO.Password);
                //user.Phone = userDTO.Phone;
                //user.RoleId = "R1";
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

        public async Task<dynamic> GetUserAccountAsync(LoginDTO loginDTO)
        {
            try
            {
                if (string.IsNullOrEmpty(loginDTO.Email))
                {
                    return Result.Failure(LoginErrors.EmailIsEmpty);
                }
                if (string.IsNullOrEmpty(loginDTO.Password))
                {
                    return Result.Failure(LoginErrors.PasswordIsEmpty);
                }
                if (!await CheckEmailExistAsync(loginDTO.Email))
                {
                    return Result.Failure(LoginErrors.EmailNotExist(loginDTO.Email));
                }
                else
                {
                    //User result = await _userRepository.GetUserAccountAsync(loginDTO.Email, Encryption.Encrypt(loginDTO.Password));
                    var result = await _userRepository.GetUserAccountAsync2(loginDTO.Email, Encryption.Encrypt(loginDTO.Password));

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
                        List<Claim> authClaims = new List<Claim>
                      {
                            new Claim("UserId", result.UserId),
                            //new Claim("Firstname",result.Firstname),
                            //new Claim("Lastname",result.Lastname),
                            new Claim(ClaimTypes.Email,result.Email),
                            new Claim("Role",result.RoleName),
                      };
                        var token = Common.TokenHepler.Instance.CreateToken(authClaims, _configuration);
                        //string avatar = await _userDetailRepository.GetAvatarByUserIdAsync(result.UserId);
                        var responseObject = new ResponseObject() {
                            UserId = result.UserId,
                            Avatar = result.Avatar,
                            Email = result.Email,
                            FullName = result.Lastname + " " + result.Firstname,
                            Token = token
                        };
                        return responseObject;
                    }
                }
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
        public async Task<dynamic> GetInforUserAsync(string userId)
        {
            try
            {
                var result = await _userDetailRepository.GetInforUserAsync(userId);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task<dynamic> UpdateUserAsync(UpdateUserObject updateUser)
        {
            try
            {
                var currentUser = await _userDetailRepository.GetInforUserAsync(updateUser.UserId);
                List<Error> errors = new List<Error>();
                var user = new User();
                var userDetail = new UserDetail();
                if (!string.IsNullOrEmpty(updateUser.Firstname))
                {
                    if (Validator.IsValidName(updateUser.Firstname))
                    {
                        user.Firstname = updateUser.Firstname;
                    }
                    else
                    {
                        errors.Add(UpdateUserErrors.FirstNameIsInvalid(updateUser.Firstname));
                    }                   
                }
                else { user.Firstname=currentUser.Firstname;}
                if (!string.IsNullOrEmpty(updateUser.Lastname))
                {
                    if (Validator.IsValidName(updateUser.Lastname))
                    {
                        user.Lastname = updateUser.Lastname;
                    }
                    else
                    {
                        errors.Add(UpdateUserErrors.LastNameIsInvalid(updateUser.Lastname));
                    }
                }
                else
                {
                    user.Lastname=currentUser.Lastname;
                }
                if (!string.IsNullOrEmpty(updateUser.Email))
                {
                    if(Validator.IsValidEmail(updateUser.Email))
                    {
                        if(!await CheckEmailExistAsync(updateUser.Email))
                        {
                            user.Email = updateUser.Email;
                        }
                        else
                        {
                            errors.Add(UpdateUserErrors.EmailAlreadyUsed(updateUser.Email));
                        }                        
                    }
                    else
                    {
                        errors.Add(UpdateUserErrors.EmailIsInvalid(updateUser.Email));
                    }
                }
                else
                {
                    user.Email=currentUser.Email;
                }
                if(!string.IsNullOrEmpty(updateUser.Phone))
                {
                    if (Validator.IsValidPhone(updateUser.Phone))
                    {
                        if(!await CheckPhoneExistAsync(updateUser.Phone))
                        {
                            userDetail.Phone = updateUser.Phone;
                        }
                        else
                        {
                            errors.Add(UpdateUserErrors.PhoneAlreadyUsed(updateUser.Phone));
                        }                        
                    }
                    else
                    {
                        errors.Add(UpdateUserErrors.PhoneIsInvalid(updateUser.Phone));
                    }
                }
                else
                {
                    userDetail.Phone=currentUser.Phone;
                }
                if(!string.IsNullOrEmpty(updateUser.PersonalId))
                {
                    if (!string.IsNullOrEmpty(updateUser.PersonalId))
                    {
                        if (Validator.IsValidPersonalId(updateUser.PersonalId))
                        {
                            if(!await CheckPersonalIdExistAsync(updateUser.PersonalId))
                            {
                                userDetail.PersonalId = updateUser.PersonalId;
                            }
                            else
                            {
                                errors.Add(UpdateUserErrors.PersonalIdAlreadyUsed(updateUser.PersonalId));
                            }
                        }
                        else
                        {
                            errors.Add(UpdateUserErrors.PersonalIdIsInvalid(updateUser.PersonalId));
                        }
                    }
                    else
                    {
                        userDetail.PersonalId = currentUser.PersonalId;
                    }
                }
                userDetail.Avatar = !string.IsNullOrEmpty(updateUser.Avatar) ? updateUser.Avatar : currentUser.Avatar;
                userDetail.Address = !string.IsNullOrEmpty(updateUser.Address) ? updateUser.Address : currentUser.Address;
                userDetail.Gender = !string.IsNullOrEmpty(updateUser.Gender) ? updateUser.Gender : currentUser.Gender;
                userDetail.TaxIdentificationNumber = !string.IsNullOrEmpty(updateUser.TaxIdentificationNumber) ? updateUser.TaxIdentificationNumber : currentUser.TaxIdentificationNumber;
                if (!string.IsNullOrEmpty(updateUser.DateOfBirth))
                {
                    if (DateTime.TryParseExact(updateUser.DateOfBirth, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dob))
                    {
                        userDetail.DateOfBirth = new DateOnly(dob.Year, dob.Month, dob.Day);
                    }
                    else
                    {
                        errors.Add(new Error("User.InvalidFormat", "Invalid date format. Expected format: dd-MM-yyyy"));
                    }
                }
                else
                {
                    userDetail.DateOfBirth = currentUser.DateOfBirth;
                }

                if (errors.All(error => error == null))
                {
                    user.UserId = updateUser.UserId;
                    userDetail.UserId = updateUser.UserId;
                    var result =await _userDetailRepository.UpdateUserDetailAsync(user,userDetail);
                    var result1 =  UpdateIsActive(user.UserId);
                    return result1 + result;
                    // 2 => isActive
                    // 1 update success
                    
                }
                    return Result.Failures(errors);
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
        private async Task<string> AutoGenerateUserDetailId()
        {
            string newuserid = "";
            string latestUserId = await _userDetailRepository.GetLatestUserDetailIdAsync();
            if (latestUserId.IsNullOrEmpty())
            {
                newuserid = "UD00000000";
            }
            else
            {
                int numericpart = int.Parse(latestUserId.Substring(2));
                int newnumericpart = numericpart + 1;
                newuserid = $"UD{newnumericpart:d8}";
            }
            return newuserid;
        }
        public async Task<bool> CheckEmailExistAsync(string email)
        {
            string existingEmail = await _userRepository.GetExistEmailAsync(email);
            return email.Equals(existingEmail);
        }
        public async Task<bool> CheckPhoneExistAsync(string phone)
        {
            string existingPhone = await _userRepository.GetExistPhoneAsync(phone);
            return phone.Equals(existingPhone);
        }
        public async Task<bool> CheckPersonalIdExistAsync(string personalId)
        {
            string existingPersonalId = await _userRepository.GetExistPersonalIdAsync(personalId);
            return personalId.Equals(existingPersonalId);
        }
        public async Task<int> UpdateUserRole(string userId, string roleId)
        {
            try
            {
                var result = await _userRepository.UpdateUserRole(userId, roleId);
                return result;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message, e);
            }
        }
        public async Task<int> UpdateIsActive(string userId)
        {
            try
            {
                var user = await _userDetailRepository.GetInforUserAsync(userId);
                if (user.Firstname != null 
                    && user.Lastname != null
                    && user.Email != null
                    && user.Phone != null
                    && user.PersonalId != null
                    && user.Avatar != null
                    && user.DateOfBirth != null
                    && user.Address != null
                    && user.Gender != null
                    && user.TaxIdentificationNumber != null
                    )
                {
                    var result = await _userRepository.UpdateIsActive(userId);
                    return result;
                }
                else
                {
                    return 0;
                }
                
                
            }
            catch (Exception e)
            {

                throw new Exception(e.Message, e);
            }
        }
    }

}
//if (!Validator.IsValidUsername(registerDTO.Username))
//{
//    return Result.Failure(RegisterErrors.UsernameIsInvalid(registerDTO.Username));

//}
//else if (await CheckUserName(registerDTO.Username))
//{
//    return Result.Failure(RegisterErrors.UserAlreadyExist(registerDTO.Username));
//}
//else
//{
//    //user.Username = registerDTO.Username;
//}
////PHONE//
//if (!Validator.IsValidPhone(registerDTO.Phone))
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
//public bool CheckEmail(string email) 
//{ 
//    if(email.Equals(_userRepository.GetEmailAsync(email))) { return true; } else { return false; }
//}
//public async Task<bool> CheckEmail(string email)
//{
//    string existingEmail = await _userRepository.GetEmailAsync(email);
//    return email.Equals(existingEmail);
//}

//public bool CheckPhone(string phone)
//{
//    if(phone.Equals(_userRepository.GetPhoneAsync(phone))) { return true; } else { return false;}
//}
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
//        var authClaims = new List<Claim>
//{                    
//    new Claim("UserId", result.UserId),
//    new Claim(ClaimTypes.Email,result.Email),                    
//    new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
//    new Claim(ClaimTypes.Role,result.RoleId)


//};

//        var authenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
//        //var authenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("nX9IwCQbu6IEQWVFZijgk8miXIZtZ9PGGQyamYGcyl2Oq1xr5wUgDYBmfkuUPxeMIBE1CnRCE3yZIdFXWgJo4V1frk4dFGup6Nyy"));
//        var token = new JwtSecurityToken(
//            issuer: _configuration["JWT:ValidIssuer"],
//            audience: _configuration["JWT:ValidAudience"],
//            expires: DateTime.UtcNow.AddDays(60),
//            claims : authClaims,
//            signingCredentials: new SigningCredentials(authenKey, SecurityAlgorithms.HmacSha256)
//            ) ;
//        return new JwtSecurityTokenHandler().WriteToken(token);
//public async Task<bool> CheckUserName(string userName)
//{
//    string existingUsername = await _userRepository.GetUserNameAsync(userName);
//    return userName.Equals(existingUsername);
//}


//public async Task<bool> CheckPhone(string phone)
//{
//    string existingPhone = await _userRepository.GetPhoneAsync(phone);
//    return phone.Equals(existingPhone);
//}
//List<Error> errors = new List<Error>();
//User user = new User();
//bool isValidName = true;
//if(!string.IsNullOrEmpty(registerDTO.Firstname))
//{
//    //check empty
//    errors.Add(null);
//    if (!Validator.IsValidUsername(registerDTO.Firstname))
//    {
//        errors.Add(RegisterErrors.UsernameIsInvalid(registerDTO.Firstname));
//        //return Result.Failure(RegisterErrors.UsernameIsInvalid(registerDTO.Firstname));
//        isValidName = false;
//    }
//    else { errors.Add(null); }

//}
//else
//{
//    errors.Add(RegisterErrors.FirstNameIsEmpty);
//    errors.Add(null);
//}
//if(!string.IsNullOrEmpty(registerDTO.Lastname))
//{
//    //check empty
//    errors.Add(null);
//    if (!Validator.IsValidUsername(registerDTO.Lastname))
//    {
//        errors.Add(RegisterErrors.UsernameIsInvalid(registerDTO.Lastname));
//        //return Result.Failure(RegisterErrors.UsernameIsInvalid(registerDTO.Lastname));
//        isValidName = false;
//    }
//    else
//    {
//        //check valid
//        errors.Add(null);
//    }
//}
//else
//{
//    errors.Add(RegisterErrors.LastNameIsEmpty);
//    errors.Add(null);
//}
//if(isValidName)
//{
//    if (await CheckFirstNameAndLastNameExistAsync(registerDTO.Lastname, registerDTO.Firstname))
//    {
//        errors.Add(RegisterErrors.UserAlreadyExist(registerDTO.Firstname, registerDTO.Lastname));
//    }
//}
//else
//{
//    errors.Add(null);
//}
////else if(errors.Count == 0)
////{
////    user.Firstname = registerDTO.Firstname;
////    user.Lastname = registerDTO.Lastname;
////}
////Email///
//if(!string.IsNullOrEmpty(registerDTO.Email))
//{
//    errors.Add(null);
//    if (!Validator.IsValidEmail(registerDTO.Email))
//    {
//        errors.Add(RegisterErrors.EmailIsInvalid(registerDTO.Email));
//        //return Result.Failure(RegisterErrors.EmailIsInvalid(registerDTO.Email));
//    }
//    else { errors.Add(null); }

//     if (await CheckEmailExistAsync(registerDTO.Email))
//    {
//        errors.Add(RegisterErrors.EmailAlreadyUsed(registerDTO.Email));
//        //return Result.Failure(RegisterErrors.EmailAlreadyUsed(registerDTO.Email));
//    }
//    else { errors.Add(null); }
//}
//else
//{
//    errors.Add(RegisterErrors.EmailIsEmpty);
//    errors.Add(null);
//    errors.Add(null);
//}

////else { user.Email = registerDTO.Email;}
////PASSWORD///
//if (!string.IsNullOrEmpty(registerDTO.Password))
//{
//    errors.Add(null);
//    if (!Validator.IsValidPassword(registerDTO.Password))
//    {
//        errors.Add(RegisterErrors.PasswordIsInvalid(registerDTO.Password));
//        //return Result.Failure(RegisterErrors.PasswordIsInvalid(registerDTO.Password));
//    }
//    else
//    {
//        errors.Add(null);
//    }
//}
//else { errors.Add(RegisterErrors.PasswordIsEmpty); errors.Add(null); }
//if(!string.IsNullOrEmpty(registerDTO.ConfirmPassword))
//{
//    errors.Add(null);
//if (!registerDTO.Password.Equals(registerDTO.ConfirmPassword))
//    {
//        errors.Add(RegisterErrors.ConfirmPasswordIsInvalid);
//        //return Result.Failure(RegisterErrors.ConfirmPasswordIsInvalid);
//    }
//    else
//    {
//        errors.Add(null);
//    }
//}
//else
//{
//    errors.Add(RegisterErrors.ConfirmPasswordIsEmpty);
//    errors.Add(null);
//}

////else
////{
////    user.Password = Encryption.Encrypt(registerDTO.Password);
////}
///                // Check if Firstname and Lastname exist
//if (isValidName)
//{
//    if (await CheckFirstNameAndLastNameExistAsync(registerDTO.Lastname, registerDTO.Firstname))
//    {
//        errors.Add(RegisterErrors.UserAlreadyExist(registerDTO.Firstname, registerDTO.Lastname));
//    }
//    else
//    {
//        errors.Add(null);
//    }
//}
//else
//{
//    errors.Add(null);
//}
//private async Task<bool>CheckFirstNameExistAsync (string firstname)
//{
//    string existFirstname = await _userRepository.GetColumnString("Firstname", firstname);
//    if(existFirstname.IsNullOrEmpty()) { return false; } else
//    {
//        return firstname.Equals(existFirstname);
//    }

//}
//private async Task<bool> CheckLastNameExistAsync(string lastname)
//{
//    string existLastname = await _userRepository.GetColumnString("Lastname", lastname);
//        return lastname.Equals(existLastname);           
//}
//private async Task<bool> CheckFirstNameAndLastNameExistAsync(string lastName, string firstName)
//{
//    var existName = await _userRepository.GetLastNameAndFirstName(lastName, firstName);
//    if (existName != null && existName.Lastname == lastName && existName.Firstname == firstName)
//    {

//        return true;
//    }
//    else
//    {

//        return false;
//    }
//}