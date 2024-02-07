using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPSS.Data.Models.DTO;

namespace TPSS.Business.Exceptions.ErrorHandler
{
    public static class RegisterErrors
    {
        public static Error UserAlreadyExist(string username) => new(
    "User.NotFound", $"The user with Username '{username}' already exist!!");

        public static Error UsernameIsInvalid(string username) => new(
"User.NotFound", $"The username:'{username}' is invalid!!!");
        public static Error PhoneAlreadyUsed(string phone) => new(
     "User.NotFound", $"The phone number:'{phone}' already used!!");
        public static Error PhoneIsInvalid(string phone) => new(
     "User.NotFound", $"The phone number:'{phone}' is invalid!!");

        public static Error EmailAlreadyUsed(string email) => new(
     "User.NotFound", $"The email:'{email}' already used!!");

        public static Error EmailIsInvalid(string email) => new(
     "User.NotFound", $"The email:'{email}' is invalid!!");

        public static Error PasswordIsInvalid(string password) => new(
     "User.NotFound", $"The password is is invalid form!!\n" +
            $"Has minimum 8 characters in length\n" +
            $"At least one uppercase English letter\n" +
            $"At least one lowercase English letter\n" +
            $"At least one digit\n" +
            $"At least one special character (#?!@$%^&*-,...)\n");
        public static Error ConfirmPasswordIsInvalid  => new(
     "User.NotFound", $"The confim password is invalid!!");


    }
}
