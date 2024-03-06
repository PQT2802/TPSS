using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using TPSS.Data.Helper;
using TPSS.Data.Models.Entities;

namespace TPSS.Data.Repository.Impl
{
    public class ContractRepository : BaseRepository, IContractRepository
    {
        public ContractRepository(IConfiguration configuration) : base(configuration)
        {
        }
        public async Task<int> CreateContractAsync(string contractId,string reservationId)
        {
            try
            {
                var query = @"
INSERT INTO Contract(ContractId, ReservationId) 
VALUES (ContractId = @contractIdValue, ReservationId = @reservationIdValue) ";
                var parameter = new DynamicParameters();
                parameter.Add("contractIdValue", contractId);
                parameter.Add("reservationIdValue", reservationId);
                using var connection = CreateConnection();
                return await connection.ExecuteAsync(query, parameter);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        public async Task<IEnumerable<Contract>> GetContractsByReservationIdAsync(string reservationId)
        {
            try
            {
                var query = "SELECT * " +
                    "FROM Contract " +
                    "WHERE ReservationId = @reservationIdValue ";
                var parameter = new DynamicParameters();
                parameter.Add("reservationIdValue", reservationId);
                using var connection = CreateConnection();
                return await connection.QueryAsync<Contract>(query, parameter);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
    }
}
