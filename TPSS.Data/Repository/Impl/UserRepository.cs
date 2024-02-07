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
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<int> CreateUserAsync(User newUser)
        {
            try
            {
                var query = "INSERT INTO [User](UserId, Email, Password, Username, Phone, isDelete) " +
                    "VALUES(@UserId, @Email, @Password, @Username, @Phone, @isDelete)";

                var parameter = new DynamicParameters();
                parameter.Add("UserId", newUser.UserId, DbType.String);
                parameter.Add("Email", newUser.Email, DbType.String);
                parameter.Add("Password", newUser.Password, DbType.String);
                parameter.Add("Username", newUser.Username, DbType.String);
                parameter.Add("Phone", newUser.Phone, DbType.String);
                parameter.Add("isDelete", newUser.IsDelete, DbType.Boolean);
                using var connection = CreateConnection();

                return await connection.ExecuteAsync(query, parameter);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message, e);
            }
        }
        //Delete = Update IsDelete
        public async Task<int> DeleteUserByIdAsync(string id)
        {
            try
            {
                var query = "UPDATE [User] " +
                    "SET IsDelete = 1" +// Explicitly set boolean value to 1
                    "WHERE [UserId] = @UserId";
                var parameter = new DynamicParameters();
                parameter.Add("UserId", id, DbType.String);
                using var connection = CreateConnection();
                return await connection.ExecuteAsync(query, parameter);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task<User> GetUserByIdAsync(string id)
        {
            try
            {
                var query = "SELECT * " + 
                    "FROM [User]" +
                    "WHERE UserId = @UserId";
                var parameter = new DynamicParameters();
                parameter.Add("UserId", id, DbType.String);
                using var connection = CreateConnection();
                return await connection.QuerySingleAsync<User>(query, parameter);//exactly one result if many => error
                                                                                 //return null if not data correct
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        
        public async Task<int> UpdateUserAsync(User updateUser)
        {
            var query = "UPDATE [User]" +
                "SET Email = @Email, Password = @Password, Username = @Username, Phone = @Phone" +
                "WHERE [UserId] = @UserId";

            var parameter = new DynamicParameters();
            parameter.Add("Email", updateUser.Email, DbType.String);
            parameter.Add("Password", updateUser.Password, DbType.String);
            parameter.Add("Username", updateUser.Username, DbType.String);
            parameter.Add("Phone", updateUser.Phone, DbType.String);
            parameter.Add("UserId", updateUser.UserId, DbType.String);
            using var connection = CreateConnection();
            return await connection.ExecuteAsync(query, parameter);
        }

        public async Task<string> GetLatestUserIdAsync()
        {
            try
            {
                var query = "SELECT TOP 1 UserId " +
                    "FROM [User] " +
                    "ORDER BY " +
                    "CAST(SUBSTRING(UserId, 8, LEN(UserId)) AS INT) DESC, " +
                    "UserId DESC";
                using var connection = CreateConnection();
                return await connection.QueryFirstOrDefaultAsync<string>(query); //retrieving the first result if have many result
                                                                                 //null if not.
            }
            catch (Exception e)
            {

                throw new Exception(e.Message, e);
            }
        }

        public async Task<string> GetUserNameAsync(string username)
        {
            try
            {
                var query = "SELECT Username " +
                    "FROM [User] " +
                    "WHERE Username = @Username";
                var parameter = new DynamicParameters();
                parameter.Add("Username", username, DbType.String);
                using var connection = CreateConnection();
                return await connection.QuerySingleAsync<string>(query, parameter);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message, e);
            }
        }
        public async Task<string> GetEmailAsync(string email)
        {
            try
            {
                var query = "SELECT Email " +
                    "FROM [User] " +
                    "WHERE Email = @Email";
                var parameter = new DynamicParameters();
                parameter.Add("Email", email, DbType.String);
                using var connection = CreateConnection();
                return await connection.QuerySingleAsync<string>(query, parameter);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message, e);
            }
        }
        public async Task<string> GetPhoneAsync(string phone)
        {
            try
            {
                var query = "SELECT Phone " +
                    "FROM [User] " +
                    "WHERE Phone = @Phone";
                var parameter = new DynamicParameters();
                parameter.Add("Phone", phone, DbType.String);
                using var connection = CreateConnection();
                return await connection.QuerySingleAsync<string>(query,parameter);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message, e);
            }
        }

    }


}
