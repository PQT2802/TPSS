using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPSS.Data.Models.DTO;
using TPSS.Data.Models.Entities;

namespace TPSS.Business.Service
{
    public interface IUserService
    {
        public  Task<User> GetUserById(String id);
        public  Task<int>  CreateUser(UserDTO user);
        public Task<int> UpdateUser(UserDTO user);
        public Task<int> DeleteUser(String id);
    }
}
