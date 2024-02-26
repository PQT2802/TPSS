using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPSS.Data.Models.DTO;

namespace TPSS.Business.Exceptions.ErrorHandler
{
    public static class UpdateUserErrors
    {       
        public static Error FirstNameIsInvalid(string firstName) => new(
        "User.FirstNameInvalid", $"The first name:'{firstName}' is invalid!!!");
        public static Error LastNameIsInvalid(string lastName) => new(
        "User.LastNameInvalid", $"The last name:'{lastName}' is invalid!!!");
        public static Error EmailAlreadyUsed(string email) => new(
     "User.EmailAlreadyUsed", $"The email:'{email}' already used!!");
        public static Error EmailIsInvalid(string email) => new(
     "User.EmailIsInvalid", $"The email:'{email}' is invalid!!");
        public static Error PhoneIsInvalid(string phone) => new(
     "User.PhoneIsInvalid", $"The phone number:'{phone}' is invalid!!");
        public static Error PhoneAlreadyUsed(string phone) => new(
     "User.PhoneAlreadyUsed", $"The phone number:'{phone}' already used!!");

    }
}
