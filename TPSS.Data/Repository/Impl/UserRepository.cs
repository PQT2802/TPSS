using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata;
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
                var query = "INSERT INTO [User] (UserId, Email, Password, Firstname, Lastname, RoleId, IsActive, IsDelete) " +
                    "VALUES(@UserId, @Email, @Password, @Firstname, @Lastname, @RoleId, @IsActive, @IsDelete)";
                var parameter = new DynamicParameters();
                parameter.Add("UserId", newUser.UserId, DbType.String);
                parameter.Add("Email", newUser.Email, DbType.String);
                parameter.Add("Password", newUser.Password, DbType.String);
                parameter.Add("Firstname", newUser.Firstname, DbType.String);
                parameter.Add("Lastname", newUser.Lastname, DbType.String);
                parameter.Add("RoleId", newUser.RoleId, DbType.String);
                parameter.Add("IsActive", newUser.IsActive, DbType.Boolean);
                parameter.Add("IsDelete", newUser.IsDelete, DbType.Boolean);
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
                    "SET IsDelete = true" +
                    "WHERE UserId = @UserId";
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
                var query = "SELECT *" +
                    "FROM [User]" +
                    "WHERE UserId = @UserId";
                var parameter = new DynamicParameters();
                parameter.Add("UserId", id, DbType.String);
                using var connection = CreateConnection();
                return await connection.QuerySingleAsync<User>(query, parameter);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task<int> UpdateUserAsync(User updateUser)
        {
            //var query = "UPDATE [User]" +
            //    "SET Email = @Email, Password = @Password, Username = @Username, Phone = @Phone" +
            //    "WHERE UserId = @UserId";

            //var parameter = new DynamicParameters();
            //parameter.Add("Email", updateUser.Email, DbType.String);
            //parameter.Add("Password", updateUser.Password, DbType.String);
            //parameter.Add("Username", updateUser.Username, DbType.String);
            //parameter.Add("Phone", updateUser.Phone, DbType.String);
            //parameter.Add("UserId", updateUser.UserId, DbType.String);
            //using var connection = CreateConnection();
            //return await connection.ExecuteAsync(query, parameter);
            return 0;
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
                return await connection.QuerySingleOrDefaultAsync<string>(query);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message, e);
            }
        }

 

        public async Task<User> GetUserAccountAsync(string email, string password)
        {
            try
            {
                var query = "SELECT UserId, Firstname, Lastname, Email " +
                            "FROM [User] " +
                            "WHERE Email = @Email AND Password = @Password";
                var parameter = new DynamicParameters();
                parameter.Add("Email", email, DbType.String);
                parameter.Add("Password", password, DbType.String);
                using var connection = CreateConnection();
                return await connection.QuerySingleOrDefaultAsync<User>(query, parameter);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message, e);
            }
        }

        public async Task<dynamic> GetUserAccountAsync2(string email, string password)
        {
            try
            {
                var query = @"
            SELECT u.UserId, u.Firstname, u.Lastname, u.Email, ud.Avatar, r.RoleName, u.IsDelete
            FROM [User] u
            INNER JOIN UserDetail ud ON u.UserId = ud.UserId
            INNER JOIN Role r ON u.RoleId = r.RoleId
            WHERE u.Email = @Email AND u.Password = @Password";

                var parameter = new DynamicParameters();
                parameter.Add("Email", email, DbType.String);
                parameter.Add("Password", password, DbType.String);

                using var connection = CreateConnection();
                return await connection.QuerySingleOrDefaultAsync<dynamic>(query, parameter);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task<int> CreateUserAsync2(Object newUser)
        {
            try
            {
                var query = "BEGIN TRANSACTION; " +
                    "INSERT INTO [User] (UserId, Email, Password, Firstname, Lastname, RoleId, IsActive, IsDelete) " +
                    "VALUES(@UserId, @Email, @Password, @Firstname, @Lastname, @RoleId, @IsActive, @IsDelete); " +
                    "INSERT INTO UserDetail (Phone, PersonalId, Avatar, UserId, DateOfBirth, Address, Gender, CreateDate, UpdateDate, CreateBy, UpdateBy, TaxIdentificationNumber, UserDetailId) " +
            "VALUES (@Phone, @PersonalId, @Avatar, @UserId, @DateOfBirth, @Address, @Gender, @CreateDate, @UpdateDate, @CreateBy, @UpdateBy, @TaxIdentificationNumber, @UserDetailId); " +
            "COMMIT";
                return 0;

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
                    $"FROM [User] " +
                    $"WHERE {columnName} = @value ";
                var parameter = new DynamicParameters();
                parameter.Add("value", value, DbType.String);
                using var connection = CreateConnection();
                return await connection.QueryFirstOrDefaultAsync<string>(query, parameter);            
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        public async Task<string> GetRoleName(string roleId)
        {
            try
            {
                var query = "SELECT roleName " +
                    "FROM [Role] " +
                    "WHERE roleId = @roleId";
                var parameter = new DynamicParameters();
                parameter.Add("roleId", roleId, DbType.String);
                using var connection = CreateConnection();
                return await connection.QuerySingleOrDefaultAsync<string>(query, parameter);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        public async Task<dynamic> GetLastNameAndFirstName(string lastName, string firstname)
        {
            try
            {
                var query = "SELECT Lastname, Firstname " +
                    "FROM [User] " +
                    "WHERE Lastname = @LastnameValue AND Firstname = @FirstnameValue ";
                var parameter = new DynamicParameters();
                parameter.Add("LastnameValue", lastName, DbType.String);
                parameter.Add("FirstnameValue", firstname, DbType.String);
                using var connection = CreateConnection();
                return await connection.QuerySingleOrDefaultAsync<dynamic>(query, parameter);

            }
            catch (Exception e)
            {

                throw new Exception(e.Message, e);
            }
        }
        public async Task<string> GetExistEmailAsync(string email)
        {
            try
            {
                var query = "SELECT Email " +
                    "FROM [User] " +
                    "WHERE Email = @emailValue AND IsDelete = 0 ";
                var parameter = new DynamicParameters();
                parameter.Add("emailValue", email);
                using var connection = CreateConnection();
                return await connection.QuerySingleOrDefaultAsync<string>(query, parameter);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message, e);
            }
        }
        public async Task<string> GetExistPhoneAsync(string phone)
        {
            try
            {
                var query = "SELECT Phone " +
                    "FROM [UserDetail] " +
                    "WHERE Phone = @phoneValue AND IsDelete = 0 ";
                var parameter = new DynamicParameters();
                parameter.Add("phoneValue", phone);
                using var connection = CreateConnection();
                return await connection.QuerySingleOrDefaultAsync<string>(query, parameter);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message, e);
            }
        }
        public async Task<string> GetExistPersonalIdAsync(string personalId)
        {
            try
            {
                var query = "SELECT PersonalId " +
                    "FROM [UserDetail] " +
                    "WHERE PersonalId = @personalIdValue AND IsDelete = 0 ";
                var parameter = new DynamicParameters();
                parameter.Add("personalIdValue", personalId);
                using var connection = CreateConnection();
                return await connection.QuerySingleOrDefaultAsync<string>(query, parameter);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message, e);
            }
        }
        public async Task<int> UpdateUserRole(string userId,string roleId)
        {
            try
            {
                var query = "UPDATE [User] " +
                    "SET RoleId = @roleIdValue " +
                    "WHERE UserId = @userIdValue ";
                var parameter = new DynamicParameters();
                parameter.Add("roleIdValue", roleId);
                parameter.Add("userIdValue", userId);
                using var connection = CreateConnection();
                return await connection.ExecuteAsync(query, parameter);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message, e);
            }
        }
        public async Task<int> UpdateIsActive(string userId)
        {
            try
            {
                var query = "UPDATE [User] " +
                    "SET IsActive = 1 " +
                    "WHERE UserId = @userIdValue ";
                var parameter = new DynamicParameters();
                parameter.Add("userIdValue", userId);
                using var connection = CreateConnection();
                return await connection.ExecuteAsync(query, parameter);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message, e);
            }
        }
    }
}
//public async Task<string> GetUserNameAsync(string username)
//{
//    try
//    {
//        var query = "SELECT Username " +
//            "FROM [User] " +
//            "WHERE Username = @Username";
//        var parameter = new DynamicParameters();
//        parameter.Add("Username", username, DbType.String);
//        using var connection = CreateConnection();
//        return await connection.QuerySingleAsync<string>(query, parameter);
//    }
//    catch (Exception e)
//    {

//        throw new Exception(e.Message, e);
//    }
//}
//public async Task<string> GetEmailAsync(string email)
//{
//    try
//    {
//        var query = "SELECT Email " +
//            "FROM [User] " +
//            "WHERE Email = @Email";
//        var parameter = new DynamicParameters();
//        parameter.Add("Email", email, DbType.String);
//        using var connection = CreateConnection();
//        return await connection.QuerySingleAsync<string>(query, parameter);
//    }
//    catch (Exception e)
//    {

//        throw new Exception(e.Message, e);
//    }
//}
//public async Task<string> GetPhoneAsync(string phone)
//{
//    try
//    {
//        var query = "SELECT Phone " +
//            "FROM [User] " +
//            "WHERE Phone = @Phone";
//        var parameter = new DynamicParameters();
//        parameter.Add("Phone", phone, DbType.String);
//        using var connection = CreateConnection();
//        return await connection.QuerySingleOrDefaultAsync<string>(query, parameter);
//    }
//    catch (Exception e)
//    {

//        throw new Exception(e.Message, e);
//    }
//}


//public async Task<User> GetUserAccountAsync(string usenameOrPhoneOrEmail, string password, string columnName)
//{
//    try
//    {
//        var query = "SELECT * " +
//            "FROM [User] " +
//            $"WHERE {columnName} = @columnName, Password = @Password";
//        var parameter = new DynamicParameters();
//        parameter.Add($"{columnName}", usenameOrPhoneOrEmail, DbType.String);
//        parameter.Add("Password", password, DbType.String);
//        using var connection = CreateConnection();
//        return await connection.QuerySingleAsync<User>(query, parameter);
//    }
//    catch (Exception)
//    {

//        throw;
//    }
//}