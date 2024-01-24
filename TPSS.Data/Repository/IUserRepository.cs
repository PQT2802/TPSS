using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using TPSS.Data.Models.Entities;

namespace TPSS.Data.Repository
{
    public interface IUserRepository
    {
        public Task<User> GetUserByIdAsync(string id);
        public Task<string> GetLatestUserIdAsync();
        public Task<string> GetUserNameAsync(string username);
        public Task<string> GetEmailAsync(string email);
        public Task<string> GetPhoneAsync(string phone);
        public Task<int> CreateUserAsync(User newUser);
        public Task<int> UpdateUserAsync(User updateUser);
        public Task<int> DeleteUserByIdAsync(string id);

        

    }
}
