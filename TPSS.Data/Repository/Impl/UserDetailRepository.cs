using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPSS.Data.Helper;
using TPSS.Data.Models.Entities;

namespace TPSS.Data.Repository.Impl
{
    public class UserDetailRepository : BaseRepository, IUserDetailRepository
    {
        public UserDetailRepository(IConfiguration configuration) : base(configuration) { }
        public async Task<UserDetail> GetUserDetailByIdAsync(string userId)
        {
            try
            {
                var query = "SELECT * " +
                    "FROM UserDetail " +
                    "WHERE UserId = @value";
                var parameter = new DynamicParameters();
                parameter.Add("value",userId,DbType.String);
                using var connection = CreateConnection();
                return await connection.QueryFirstOrDefaultAsync<UserDetail>(query, parameter);

            }
            catch (Exception e)
            {

                throw new Exception(e.Message, e);
            }
        }

        public async Task<int> UpdateUserDetailAsynce(UserDetail updateUserDetail)
        {
            try
            {
                var query = "UPDATE UserDetail " +
                    "SET Phone =@Phone, " +
                    "PersonalId = @PersonalId, " +
                    "Avatar = @Avatar, " +
                    "DateOfBirth = @DateOfBirth, " +
                    "Address = @Address, " +
                    "Gender = @Gender, " +
                    "UpdateBy = @UpdateBy, " +
                    "UpdateDate = @UpdateDate, " +
                    "WHERE UserId = UserId ";
                var parameter = new DynamicParameters();
                parameter.Add("Phone", updateUserDetail.Phone, DbType.String);
                parameter.Add("PersonalId", updateUserDetail.PersonalId, DbType.String);
                parameter.Add("Avatar", updateUserDetail.Avatar, DbType.String);
                parameter.Add("DateOfBirth", updateUserDetail.DateOfBirth, DbType.DateTime);
                parameter.Add("Address", updateUserDetail.Phone, DbType.String);
                parameter.Add("Gender", updateUserDetail.Phone, DbType.String);
                parameter.Add("UpdateBy", updateUserDetail.Phone, DbType.String);
                parameter.Add("UpdateDate", updateUserDetail.Phone, DbType.DateTime);
                using var connection = CreateConnection();
                return await connection.ExecuteAsync(query, parameter);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message, e);
            }



        }

        public async Task<string> GetColumnString(string columnName, string value)
        {
            try
            {
                var query = $"SELECT {columnName} " +
                    $"FROM [UserDetail] " +
                    $"WHERE {columnName} = @value ";
                var parameter = new DynamicParameters();
                parameter.Add("value", value, DbType.String);
                using var connection = CreateConnection();
                return await connection.QuerySingleOrDefaultAsync<string>(query, parameter);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task<int> CreateUserDetailAsync(UserDetail userDetail)
        {
            try
            {
                var query = "INSERT INTO UserDetail (Phone, PersonalId, Avatar, UserId, DateOfBirth, Address, Gender, CreateDate, UpdateDate, CreateBy, UpdateBy, TaxIdentificationNumber, UserDetailId) "+
            "VALUES (@Phone, @PersonalId, @Avatar, @UserId, @DateOfBirth, @Address, @Gender, @CreateDate, @UpdateDate, @CreateBy, @UpdateBy, @TaxIdentificationNumber, @UserDetailId)";
                var parameter = new DynamicParameters();
                parameter.Add("Phone", userDetail.Phone, DbType.String);
                parameter.Add("PersonalId", userDetail.PersonalId, DbType.String);
                parameter.Add("Avatar", userDetail.Avatar, DbType.String);
                parameter.Add("UserId", userDetail.UserId, DbType.String);
                parameter.Add("DateOfBirth", userDetail.DateOfBirth, DbType.Date);
                parameter.Add("Address", userDetail.Avatar, DbType.String);
                parameter.Add("Gender", userDetail.Gender, DbType.String);
                parameter.Add("CreateDate", userDetail.CreateDate, DbType.DateTime);
                parameter.Add("UpdateDate", userDetail.UpdateDate, DbType.DateTime);
                parameter.Add("CreateBy", userDetail.CreateBy, DbType.String);
                parameter.Add("UpdateBy", userDetail.UpdateBy, DbType.String);
                parameter.Add("TaxIdentificationNumber", userDetail.TaxIdentificationNumber, DbType.String);
                parameter.Add("UserDetailId", userDetail.UserDetailId, DbType.String);
                using var connection = CreateConnection();
                return await connection.ExecuteAsync(query, parameter);

            }
            catch (Exception e)
            {

                throw new Exception(e.Message, e);
            }
        }
        public async Task<string> GetAvatarByUserIdAsync(string userId)
        {
            var query = "SELECT Avatar " +
                "FROM UserDetail " +
                "WHERE UserId = @UserIdValue";
            var parameter = new DynamicParameters();
            parameter.Add("UserIdValue", userId,DbType.String);
            using var connection = CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<string>(query, parameter);
        }
    }
}
