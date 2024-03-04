﻿using System;
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
        public Task<User> GetUserAccountAsync(string email, string password);
        public Task<string> GetLatestUserIdAsync();
        public Task<int> CreateUserAsync(User newUser);
        public Task<int> UpdateUserAsync(User updateUser);
        public Task<int> DeleteUserByIdAsync(string id);
        public Task<string> GetColumnString(string columnName,string value);
        public Task<string> GetRoleName(string roleId);
        public Task<dynamic> GetUserAccountAsync2(string email, string password);
        public Task<int> CreateUserAsync2(Object newUser);
        public Task<dynamic> GetLastNameAndFirstName(string lastName,string firstname);
        public Task<string> GetExistEmailAsync(string email);
        public Task<string> GetExistPhoneAsync(string phone);
        public Task<string> GetExistPersonalIdAsync(string personalId);
        public Task<int> UpdateUserRole(string userId,string roleId);
        public Task<int> UpdateIsActive (string userId);

    }
}
