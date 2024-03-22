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
        "User.UsernameIsInvalid", $"The username:'{username}' is invalid!!!");
        public static Error PhoneAlreadyUsed(string phone) => new(
     "User.PhoneAlreadyUsed", $"The phone number:'{phone}' already used!!");
        public static Error PhoneIsInvalid(string phone) => new(
     "User.PhoneIsInvalid", $"The phone number:'{phone}' is invalid!!");

        public static Error EmailAlreadyUsed(string email) => new(
     "User.EmailAlreadyUsed", $"The email:'{email}' already used!!");

        public static Error EmailIsInvalid(string email) => new(
     "User.EmailIsInvalid", $"The email:'{email}' is invalid!!");

        public static Error PasswordIsInvalid(string password) => new(
     "User.PasswordIsInvalid", $"The password is is invalid form!!");
        public static Error ConfirmPasswordIsInvalid  => new(
     "User.ConfirmPasswordIsInvalid", $"The confim password is invalid!!");

        public static Error FirstNameIsEmpty => new(
     "User.FirstNameIsEmpty", $"First name should not be empty!!");
        public static Error LastNameIsEmpty => new(
     "User.LastNameIsEmpty", $"Last name should not be empty!!");
        public static Error EmailIsEmpty => new(
     "User.EmailIsEmpty", $"Email should not be empty!!");
        public static Error PasswordIsEmpty => new(
     "User.PasswordIsEmpty", $"Password should not be empty!!");
        public static Error ConfirmPasswordIsEmpty => new(
     "User.ConfirmPasswordIsEmpty", $"Confirm password should not be empty!!");
    }
}
