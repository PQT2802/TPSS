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
    internal class ContractRepository : BaseRepository, IContractRepository
    {
        public ContractRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<int> CreateContractAsync(Contract Createcontract)
        {
            try
            {
                var query = "INSERT INTO [Contract] (ContractId, ReservationId, ContractDate, ContractTerms, Deposit)"
                        + "VALUES(@ContractId, @ReservationId, @ContractDate, @ContractTerms, @Deposit)";
                var parameter = new DynamicParameters();
                parameter.Add("ContractId", Createcontract.ContractId, DbType.String);
                parameter.Add("ReserevationId", Createcontract.ReservationId, DbType.String);
                parameter.Add("ContractDate", Createcontract.ContractDate, DbType.Date);
                parameter.Add("ContractTerms", Createcontract.ContractTerms, DbType.String);
                parameter.Add("Deposit", Createcontract.Deposit, DbType.Double);
                using var connection = CreateConnection();
                return await connection.ExecuteAsync(query, parameter);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task<int> DeleteContractAsync(string contractId)
        {
            try
            {
                var query = "UPDATE [Contract]"
                           + "SET IsDelete = true"
                           + "WHERE ContractId = @ContractId";
                var parameter = new DynamicParameters();
                parameter.Add("ContractId", contractId,DbType.String);
                using var connection = CreateConnection();
                return await connection.ExecuteAsync(query, parameter);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task<Contract> GetContractByIdAsync(string contractId)
        {
            try
            {
                var query = "SELECT *"
                          + "FROM [Contract]"
                          + "WHERE ContractId = @ContractId";
                var parameter = new DynamicParameters();
                parameter.Add("ContractID", contractId, DbType.String);
                using var connection = CreateConnection();
                return await connection.QuerySingleAsync<Contract>(query,parameter);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task<int> UpdateContractAsync(Contract updateContract)
        {
            try
            {
                var query = "UPDATE [Contract]"
                          + "SET ReservationId = @ReservationId, ContractDate = @ContractDate, ContractTerms = @ContractTerms, Deposit = @Deposit "
                          + "WHERE ContractId = @ContractId";
                var parameter = new DynamicParameters();
                parameter.Add("ReservationId", updateContract.ReservationId, DbType.String);
                parameter.Add("ContractDate", updateContract.ContractDate, DbType.Date);
                parameter.Add("ContractTerms", updateContract.ContractTerms, DbType.String);
                parameter.Add("Deposit", updateContract.Deposit, DbType.Double);
                parameter.Add("ContractId", updateContract.ContractId, DbType.String);
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
