using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPSS.Data.Models.Entities;

namespace TPSS.Data.Repository
{
    public interface IUserRepository
    {
        public Task<User> GetUserById(string id);
        public Task<int> CreateUser(User newUser);
        public Task<int> UpdateUser(User updateUser);
        public Task<int> DeleteUserById(string id);
    }
}
