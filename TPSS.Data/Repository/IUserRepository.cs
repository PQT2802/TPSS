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
        public Task<User> GetUserByIdAsync(string id);
        public Task<int> CreateUserAsync(User newUser);
        public Task<int> UpdateUserAsync(User updateUser);
        public Task<int> DeleteUserByIdAsync(string id);
    }
}
