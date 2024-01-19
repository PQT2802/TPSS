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
                //var query = "INSERT INTO User ( UserId, Username, Password, FirstName, LastName, Email, Phone, DigitalSignature, Verify ) " +
                //    "VALUES( @UserId, @Username, @Password, @FirstName, @LastName, @Email, @Phone, @DigitalSignature, @Verify )";
                //var parameters = new DynamicParameters();
                //parameters.Add("UserId", newUser.UserId, DbType.String);
                //parameters.Add("UserName", newUser.Username, DbType.String);
                //parameters.Add("Password", newUser.Password, DbType.String);
                //parameters.Add("FirstName", newUser.FirstName, DbType.String);
                //parameters.Add("LastName", newUser.LastName, DbType.String);
                //parameters.Add("Email", newUser.Email, DbType.String);
                //parameters.Add("Phone", newUser.Phone, DbType.String);
                //parameters.Add("DigitalSignature", newUser.DigitalSignature, DbType.String);
                //parameters.Add("Verify", newUser.Verify, DbType.String);

                //using var connection = CreateConnection();
                //return await connection.ExecuteAsync(query, parameters);
                var query = "INSERT INTO User (UserId, RoleId, Email,Password,Username)" +
                    "";

                return 1;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message, e);
            }
        }

        Task<int> IUserRepository.DeleteUserByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        Task<User> IUserRepository.GetUserByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        Task<int> IUserRepository.UpdateUserAsync(User updateUser)
        {
            throw new NotImplementedException();
        }
    }
}
