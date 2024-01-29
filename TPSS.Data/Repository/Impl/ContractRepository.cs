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
    public class ContractRepository(IConfiguration configuration) : BaseRepository(configuration), IContractRepository
    {
        public async Task<int> CreateContractAsync(Contract Createcontract)
        {
            try
            {
                var query = "INSERT INTO [Contract] (ContractId, ReservationId, ContractDate, ContractTerms, Deposit, ContractStatus, isDelete)"
                        + "VALUES(@ContractId, @ReservationId, @ContractDate, @ContractTerms, @Deposit, @ContractStatus, @isDelete)";
                        
                var parameter = new DynamicParameters();
                parameter.Add("ContractId", Createcontract.ContractId, DbType.String);
                parameter.Add("ReserevationId", Createcontract.ReservationId, DbType.String);
                parameter.Add("ContractDate", Createcontract.ContractDate, DbType.Date);
                parameter.Add("ContractTerms", Createcontract.ContractTerms, DbType.String);
                parameter.Add("Deposit", Createcontract.Deposit, DbType.Double);
                parameter.Add("ContractStatus", Createcontract.ContractStatus, DbType.String);
                parameter.Add("isDelete",Createcontract.IsDelete, DbType.Boolean);
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

        public async Task<Contract> GetContractByIdAsync(string contractId)//sill error by date
        {
            try
            {
                var query = "SELECT  "
                          + "FROM [Contract] "
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

        public async Task<DateOnly> GetDateContractByIdAsync(string ContractId) //kiem tra ngay
        {
            try
            {
                var query = "SELECT ContractDate " +
                    "FROM Contract " +
                    "WHERE ContractId = @ContractId";
                var parameter = new DynamicParameters();
                parameter.Add("ContractId", ContractId, DbType.String);
                using var connection = CreateConnection();
                DateTime dateTime = await connection.QuerySingleAsync<DateTime>(query, parameter);
                DateOnly contractDate = DateOnly.FromDateTime(dateTime);
                return contractDate;
            }
            catch (Exception e)
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

        public async Task<string> GetLatestContractIdAsync()
        {
            try
            {
                var query = "SELECT TOP 1 ContractId " +
                    "FROM [Contract] " +
                    "ORDER BY " +
                    "CAST(SUBSTRING(ContractId, 8, LEN(ContractId)) AS INT) DESC, " +
                    "ContractId DESC";
                using var connection = CreateConnection();
                return await connection.QuerySingleAsync<string>(query);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message, e);
            }
        }
    }
}
