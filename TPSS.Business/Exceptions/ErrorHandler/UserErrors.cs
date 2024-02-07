using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPSS.Data.Models.DTO;

namespace TPSS.Business.Exceptions.ErrorHandler
{
    public static class UserErrors
    {
        public static Error UserAlreadyExist(string username) => new(
            "User.NotFound", $"The user with Username '{username}' already exist!!");
    }
}
