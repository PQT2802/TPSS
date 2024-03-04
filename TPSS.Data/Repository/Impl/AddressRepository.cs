using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using TPSS.Data.Helper;
using TPSS.Data.Models.Entities;

namespace TPSS.Data.Repository.Impl
{
    public class AddressRepository : BaseRepository, IAddressRepository
    {
        public AddressRepository(IConfiguration configuration) : base(configuration)
        {
        }
        public async Task<IEnumerable<Address>> GetCity()
        {
            var query = "SELECT City " +
                "FROM [Address] ";
            using var connection = CreateConnection();
            return await connection.QueryAsync<Address>(query);
        }
        public async Task<IEnumerable<string>> GetDistrict(string addressId)
        {
            var query = "SELECT District " +
                "FROM [AddressDetail] " +
                "WHERE AddressId = @addressIdValue ";
            var parameter = new DynamicParameters();
            parameter.Add("addressIdValue", addressId);
            using var connection = CreateConnection();
            return await connection.QueryAsync<string>(query, parameter);
        }
        public async Task<IEnumerable<string>> GetWard(string addressId, string district)
        {
            var query = "SELECT Ward " +
                "FROM AddressDetail " +
                "WHERE AddressId = addressIdValue AND District = @districValue ";
            var parameter = new DynamicParameters();
            parameter.Add("addressIdValue", addressId);
            parameter.Add("districValue", district);
            using var connection = CreateConnection();
            return await connection.QueryAsync<string>(query, parameter);
        }
    }
}
