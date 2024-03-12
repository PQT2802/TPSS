using Azure;
using FirebaseAdmin.Auth;
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
using System.Net.Mail;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TPSS.Business.Common;
using TPSS.Business.Exceptions.ErrorHandler;
using TPSS.Data.Models.DTO;
using TPSS.Data.Models.Entities;
using TPSS.Data.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.SqlServer.Server;

namespace TPSS.Business.Service.Impl
{
    public sealed class UserService : IUserService
    {
        //DI
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IUserDetailRepository _userDetailRepository;
        private readonly IImageService _imageService;


        public UserService(IUserRepository userRepository, IConfiguration configuration, IUserDetailRepository userDetailRepository, IImageService imageService)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _userDetailRepository = userDetailRepository;
            _imageService = imageService;

        }


        public async Task<dynamic> RegistUserAsync(RegisterDTO registerDTO,string confirmCode)
        {
            try
            {
                List<Error> errors = new List<Error>();
                User user = new User();

                errors.AddRange(
                    string.IsNullOrEmpty(registerDTO.Firstname)
                    ? new Error[] { RegisterErrors.FirstNameIsEmpty }//User.FirstNameIsEmpty
                    : !Validator.IsValidName(registerDTO.Firstname)
                        ? new Error[] { RegisterErrors.UsernameIsInvalid(registerDTO.Firstname) }//User.UsernameIsInvalid
                        : Enumerable.Empty<Error>()
                    );
                errors.AddRange(
                    string.IsNullOrEmpty(registerDTO.Lastname)
                    ? new Error[] { RegisterErrors.UsernameIsInvalid(registerDTO.Lastname) } //User.UsernameIsInvalid
                    : !Validator.IsValidUsername(registerDTO.Lastname)
                        ? new Error[] { RegisterErrors.UsernameIsInvalid(registerDTO.Lastname) }
                        : Enumerable.Empty<Error>()
                    );
                errors.AddRange(
                    string.IsNullOrEmpty(registerDTO.Email)
                    ? new Error[] { RegisterErrors.EmailIsEmpty }
                    : !Validator.IsValidEmail(registerDTO.Email)
                      ? new Error[] { RegisterErrors.EmailIsInvalid(registerDTO.Email) }
                      : await CheckEmailExistAsync(registerDTO.Email)
                        ? new Error[] { RegisterErrors.EmailAlreadyUsed(registerDTO.Email) }
                        : string.IsNullOrEmpty(confirmCode)
                          ? new Error[] {new Error("User.CodeIsEmpty","Verify code should not be empty")}
                          : !confirmCode.Equals(TemporaryDataStorage.GetConfirmationCode(registerDTO.Email))
                             ? new Error[] { new Error("User.InvalidConfirmCode", "Your verify code is wrong!!!") }
                             : Enumerable.Empty<Error>()
                    );
                errors.AddRange(
                    string.IsNullOrEmpty(registerDTO.Password)
                    ? new Error[] { RegisterErrors.PasswordIsEmpty }
                    : !Validator.IsValidPassword(registerDTO.Password)
                      ? new Error[] { RegisterErrors.PasswordIsInvalid(registerDTO.Password) }
                      : Enumerable.Empty<Error>()
                    );
                errors.AddRange(
                    string.IsNullOrEmpty(registerDTO.ConfirmPassword)
                    ? new Error[] { RegisterErrors.ConfirmPasswordIsEmpty }
                    : !registerDTO.Password.Equals(registerDTO.ConfirmPassword)
                       ? new Error[] { RegisterErrors.ConfirmPasswordIsInvalid }
                       : Enumerable.Empty<Error>()
                    );

                if (errors == null || !errors.Any())
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
                        TemporaryDataStorage.RemoveConfirmationCode(registerDTO.Email);
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
        public async Task SendConfirmationEmail(string toEmailAddress)
        {
            // Email account information
            string smtpServer = _configuration["SMTP:Server"];
            int port = int.Parse(_configuration["SMTP:Port"]);
            string senderEmail = _configuration["Sender:Email"];
            string password = _configuration["Sender:Password"];

            // Create an SmtpClient object to send email
            SmtpClient smtpClient = new SmtpClient(smtpServer, port);
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(senderEmail, password);
            smtpClient.EnableSsl = true;

            // Generate a confirmation code using GUID
            string currentConfirmationCode = Guid.NewGuid().ToString().Substring(0, 6);

            // Email content
            string subject = "Email Confirmation";
            string body = "Your confirmation code is: " + currentConfirmationCode;

            // Create a MailMessage object
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(senderEmail);
            mailMessage.To.Add(toEmailAddress);
            mailMessage.Subject = subject;
            mailMessage.Body = body;

            // Send email
            try
            {
                smtpClient.Send(mailMessage);
                Common.TemporaryDataStorage.SaveConfirmationCode(toEmailAddress, currentConfirmationCode);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sending confirmation email: " + ex.Message);
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
                            new Claim(ClaimTypes.Role,result.RoleName),
                      };
                        var token = Common.TokenHepler.Instance.CreateToken(authClaims, _configuration);
                        var responseObject = new ResponseObject() {
                            UserId = result.UserId,
                            Avatar = result.Avatar,
                            Email = result.Email,
                            FullName = result.Lastname + " " + result.Firstname,
                            Role = result.RoleName,
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
        public async Task<ResponseObject> GetTokenFirebase(string firebaseToken)
        {
            try
            {
                FirebaseToken decryptedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(firebaseToken);
                string uid = decryptedToken.Uid;
                UserRecord userRecord = await FirebaseAuth.DefaultInstance.GetUserAsync(uid); // bien cua firebase
                string email = userRecord.Email;
                string lastName = userRecord.DisplayName;                
                string ImageUrl = userRecord.PhotoUrl.ToString();                
                User userObject = await _userRepository.GetUserByEmail(email);
                ResponseObject response = new();
                if (userObject == null)
                {
                    User user = new User();
                    user.UserId = await AutoGenerateUserId();
                    user.Email = userRecord.Email;;
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
                    userObject = await _userRepository.GetUserByEmail(email);
                }
                List<Claim> authClaims = new List<Claim>
                      {
                            new Claim("UserId", userObject.UserId),
                            new Claim(ClaimTypes.Email,email),
                            new Claim("Role","Customer"),
                      };
                var token = Common.TokenHepler.Instance.CreateToken(authClaims, _configuration);
                var responseObject = new ResponseObject()
                {
                    UserId = userObject.UserId,
                    Avatar = ImageUrl,
                    Email = email,
                    FullName = lastName,
                    Role ="Customer",
                    Token = token
                };
                return responseObject;
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
                if (currentUser.Firstname != updateUser.Firstname)
                {
                    errors.AddRange(
                        string.IsNullOrEmpty(updateUser.Lastname)
                        ? new Error[] { RegisterErrors.UsernameIsInvalid(updateUser.Lastname) } //User.UsernameIsInvalid
                        : !Validator.IsValidUsername(updateUser.Lastname)
                            ? new Error[] { RegisterErrors.UsernameIsInvalid(updateUser.Lastname) }
                            : Enumerable.Empty<Error>()
                        );
                }
                if(currentUser.Lastname != updateUser.Lastname)
                {
                    errors.AddRange(
                        string.IsNullOrEmpty(updateUser.Lastname)
                        ? new Error[] { RegisterErrors.UsernameIsInvalid(updateUser.Lastname) } //User.UsernameIsInvalid
                        : !Validator.IsValidUsername(updateUser.Lastname)
                            ? new Error[] { RegisterErrors.UsernameIsInvalid(updateUser.Lastname) }
                            : Enumerable.Empty<Error>()
                                   );
                }
                if(currentUser.Email != updateUser.Email)
                {
                    errors.AddRange(
                        string.IsNullOrEmpty(updateUser.Email)
                        ? new Error[] { RegisterErrors.EmailIsInvalid(updateUser.Email) }
                        : !Validator.IsValidEmail(updateUser.Email)
                          ? new Error[] { RegisterErrors.EmailIsInvalid(updateUser.Email) }
                          : await CheckEmailExistAsync(updateUser.Email)
                            ? new Error[] { RegisterErrors.EmailAlreadyUsed(updateUser.Email) }
                            : string.IsNullOrEmpty(updateUser.ConfirmCode)
                              ? new Error[] { new Error("User.CodeIsEmpty", "Verify code should not be empty") }
                              : !updateUser.ConfirmCode.Equals(TemporaryDataStorage.GetConfirmationCode(updateUser.Email))
                                 ? new Error[] { new Error("User.InvalidConfirmCode", "Your verify code is wrong!!!") }
                                 : Enumerable.Empty<Error>()
                        );
                }
                if(currentUser.Phone != updateUser.Phone)
                {
                    errors.AddRange(
                        string.IsNullOrEmpty(updateUser.Phone)
                        ? new Error[] { UpdateUserErrors.IsEmptyField() }
                        : !Validator.IsValidPhone(updateUser.Phone)
                          ? new Error[] { UpdateUserErrors.PhoneIsInvalid(updateUser.Phone) }
                          : await CheckPhoneExistAsync(updateUser.Phone)
                            ? new Error[] { UpdateUserErrors.PhoneAlreadyUsed(updateUser.Phone) }
                            : Enumerable.Empty<Error>()
                    );
                }
                if (currentUser.PersonalId != updateUser.PersonalId)
                {
                    errors.AddRange(
                        string.IsNullOrEmpty(updateUser.PersonalId)
                        ? new Error[] { UpdateUserErrors.IsEmptyField() }
                        : !Validator.IsValidPersonalId(updateUser.PersonalId)
                           ? new Error[] { UpdateUserErrors.PersonalIdIsInvalid(updateUser.PersonalId) }
                           : await CheckPersonalIdExistAsync(updateUser.PersonalId)
                             ? new Error[] { UpdateUserErrors.PersonalIdAlreadyUsed(updateUser.PersonalId) }
                             : Enumerable.Empty<Error>()
                        );
                }
                //if(currentUser.Avatar != updateUser.Avatar)
                //{
                //    errors.AddRange(
                //        string.IsNullOrEmpty(updateUser.Avatar)
                //        ? new Error[] { UpdateUserErrors.IsEmptyField() }
                //        : Enumerable.Empty<Error>()
                //        );
                //}
                // avatar luu 1 folder tren firebase
                if (currentUser.Address != updateUser.Address)
                {
                    errors.AddRange(
                        string.IsNullOrEmpty(updateUser.Address)
                        ? new Error[] { UpdateUserErrors.IsEmptyField() }
                        : Enumerable.Empty<Error>()
                        );
                }
                if (currentUser.Address != updateUser.TaxIdentificationNumber)
                {
                    errors.AddRange(
                        string.IsNullOrEmpty(updateUser.TaxIdentificationNumber)
                        ? new Error[] { UpdateUserErrors.IsEmptyField() }
                        : Enumerable.Empty<Error>()
                        );
                }
                if (currentUser.Gender != updateUser.Gender)
                {
                    errors.AddRange(
                        string.IsNullOrEmpty(updateUser.Gender)
                        ? new Error[] { UpdateUserErrors.IsEmptyField() }
                        : Enumerable.Empty<Error>()
                        );
                }
                    errors.AddRange(
                        string.IsNullOrEmpty(updateUser.DateOfBirth)
                        ? new Error[] { UpdateUserErrors.IsEmptyField() }
                        : !DateTime.TryParseExact(updateUser.DateOfBirth, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dob)
                          ? new Error[] { new Error("User.InvalidFormat", "Invalid date format. Expected format: dd/MM/yyyy") }
                          : Enumerable.Empty<Error>()
                        );
                

                if (errors == null || !errors.Any())
                {
                    user.UserId = updateUser.UserId;
                    user.Firstname = updateUser.Firstname;
                    user.Lastname = updateUser.Lastname;
                    user.Email = updateUser.Email;
                    
                    userDetail.UserId = updateUser.UserId;
                    userDetail.Address = updateUser.Address;
                    userDetail.Avatar = await _imageService.UploadAvatarAsync(updateUser.Avatar, "Avatar", updateUser.UserId);
                    userDetail.Phone = updateUser.Phone;
                    userDetail.Gender = updateUser.Gender;
                    userDetail.TaxIdentificationNumber = updateUser.TaxIdentificationNumber;
                    userDetail.PersonalId = updateUser.PersonalId;
                    userDetail.DateOfBirth = DateOnly.ParseExact(updateUser.DateOfBirth, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    
                    TemporaryDataStorage.RemoveConfirmationCode(updateUser.Email);
                    var result =await _userDetailRepository.UpdateUserDetailAsync(user,userDetail);
                    var result1 = await UpdateIsActive(user.UserId);
                    return result;
                        
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
        public async Task<dynamic> UpdatePasswordAsync(string userId, ChangingPasswordDTO changingPasswordDTO)
        {
            try
            {
                List<Error> errors = new List<Error>();
                var currentPassword = await _userRepository.GetColumnString("Password", Encryption.Encrypt(changingPasswordDTO.Password));
                errors.AddRange(
                    string.IsNullOrEmpty(currentPassword)
                    ? new Error[] { new Error("User.IncorrectPassword", "Incorrect password") }
                    : !Validator.IsValidPassword(changingPasswordDTO.NewPassword)
                      ? new Error[] { RegisterErrors.PasswordIsInvalid(changingPasswordDTO.NewPassword) }
                      : !changingPasswordDTO.NewPassword.Equals(changingPasswordDTO.ConfirmNewPassword)
                        ? new Error[] { new Error("User.IncorrectPassword", "Incorrect password") }
                        : Enumerable.Empty<Error>()
                    ) ;
                if ( errors.Any() )
                {
                    var result = _userRepository.UpdatePasswordAsync(userId, changingPasswordDTO.NewPassword);
                    return result;
                }
                return errors;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        public Task<IEnumerable<User>> GetAllUserAsync()
        {
            var result =_userRepository.GetAllUserAsync();
            return result;
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

//if (string.IsNullOrEmpty(registerDTO.Firstname))
//{
//    errors.Add(RegisterErrors.FirstNameIsEmpty);
//}
//else if (!Validator.IsValidName(registerDTO.Firstname))
//{
//    errors.Add(RegisterErrors.UsernameIsInvalid(registerDTO.Firstname));
//}

//if (!string.IsNullOrEmpty(registerDTO.Lastname))
//{
//    if (!Validator.IsValidUsername(registerDTO.Lastname))
//    {
//        errors.Add(RegisterErrors.UsernameIsInvalid(registerDTO.Lastname));
//    }
//}
//else
//{
//    errors.Add(RegisterErrors.LastNameIsEmpty);
//}
//if (!string.IsNullOrEmpty(registerDTO.Email))
//{
//    if (!Validator.IsValidEmail(registerDTO.Email))
//    {
//        errors.Add(RegisterErrors.EmailIsInvalid(registerDTO.Email));
//    }

//    if (await CheckEmailExistAsync(registerDTO.Email))
//    {
//        errors.Add(RegisterErrors.EmailAlreadyUsed(registerDTO.Email));
//    }
//}
//else
//{
//    errors.Add(RegisterErrors.EmailIsEmpty);
//}
//if (!string.IsNullOrEmpty(registerDTO.Password))
//{
//    errors.Add(null);
//    if (!Validator.IsValidPassword(registerDTO.Password))
//    {
//        errors.Add(RegisterErrors.PasswordIsInvalid(registerDTO.Password));
//    }
//    else
//    {
//        errors.Add(null);
//    }
//}
//else
//{
//    errors.Add(RegisterErrors.PasswordIsEmpty); // Add the error for passwordIsEmpty only if password is empty
//    errors.Add(null);
//}
//// Confirm password validation
//if (!string.IsNullOrEmpty(registerDTO.ConfirmPassword))
//{
//    errors.Add(null);
//    if (!registerDTO.Password.Equals(registerDTO.ConfirmPassword))
//    {
//        errors.Add(RegisterErrors.ConfirmPasswordIsInvalid);
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
                //if (!string.IsNullOrEmpty(updateUser.DateOfBirth))
                //{
                //    if (DateTime.TryParseExact(updateUser.DateOfBirth, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dob))
                //    {
                //        userDetail.DateOfBirth = new DateOnly(dob.Year, dob.Month, dob.Day);
                //    }
                //    else
                //    {
                //        errors.Add(new Error("User.InvalidFormat", "Invalid date format. Expected format: dd-MM-yyyy"));
                //    }
                //}
                //else
                //{
                //    userDetail.DateOfBirth = currentUser.DateOfBirth;
                //}