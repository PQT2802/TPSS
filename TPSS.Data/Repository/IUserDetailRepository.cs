﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPSS.Data.Models.Entities;

namespace TPSS.Data.Repository
{
    public interface IUserDetailRepository
    {
        public Task<int> CreateUserDetailAsync(UserDetail userDetail);
        public Task<UserDetail> GetUserDetailByIdAsync(string userId);
        public Task<int> UpdateUserDetailAsynce(UserDetail updateUserDetail);
        public Task<string> GetColumnString(string columnName, string value);
        public Task<string> GetAvatarByUserIdAsync(string userId);
    }
}
