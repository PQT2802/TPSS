using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPSS.Data.Models.DTO;

namespace TPSS.Business.Exceptions.ErrorHandler
{
    public static class LoginErrors
    {
        //    public static Error UsernameNotExist(string username) => new(
        //"User.NotFound", $"The user with Username '{username}' is not exist!!");
        public static Error EmailNotExist(string email) => new(
        "User.EmailNotExist", $"The user with Email '{email}' is not exist!!");
        public static Error PasswordIsWrong => new(
            "User.PasswordIsWrong", $"The password is wrong !!!");
        public static Error AccountIsDelete => new(
     "User.AccountIsDelete", $"The account was deleted !!!");

        public static Error EmailIsEmpty => new(
     "User.EmailIsEmpty", $"Email should not be empty !!!");
        public static Error PasswordIsEmpty => new(
     "User.PasswordIsEmpty", $"Password should not be empty !!!");
    }
}
