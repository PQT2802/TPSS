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
        public static Error UserAlreadyExist(string firstname, string lastname) => new(
    "User.NotFound", $"The user with Firstname '{firstname}' and Lastname '{lastname}' already exist!!");       

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

        public static Error FirstNameIsEmpty => new(
     "User.NotFound", $"First name should not be empty!!");
        public static Error LastNameIsEmpty => new(
     "User.NotFound", $"Last name should not be empty!!");
        public static Error EmailIsEmpty => new(
     "User.NotFound", $"Email should not be empty!!");
        public static Error PasswordIsEmpty => new(
     "User.NotFound", $"Password should not be empty!!");
        public static Error ConfirmPasswordIsEmpty => new(
     "User.NotFound", $"Confirm password should not be empty!!");
    }
}
