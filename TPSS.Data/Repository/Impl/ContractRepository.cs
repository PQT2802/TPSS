using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Net.NetworkInformation;
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
INSERT INTO [Contract](ContractId, ReservationId) 
VALUES (ContractId = @contractIdValue, ReservationId = @reservationIdValue,ContractStatus = 'Preparing' ) ";
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
                    "FROM [Contract] " +
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
                return await connection.QuerySingleOrDefaultAsync<string>(query);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message, e);
            }
        }
        public async Task<IEnumerable<dynamic>> GetAllContractAsync()
        {
            try
            {
                var query = @"
SELECT  c.ContractId,
        r.ReservationId,
        r.BuyerId,
        r.SellerId,
        r.PropertyId,
        p.PropertyTitle,
        c.Thirdparty,
        c.ContractStatus,
        c.Deposit,
        buyer.Firstname AS BuyerFirstName,
        buyer.Lastname AS BuyerLastName,
        seller.Firstname AS SellerFirstName,
        seller.Lastname AS SellerLastName,
        staff.Firstname AS StaffFirstName,
        staff.Lastname AS StaffLastName
FROM [Contract] c
INNER JOIN Reservation r ON c.ReservationId = r.ReservationId
INNER JOIN Property p ON r.PropertyId = p.PropertyId
LEFT JOIN [User] buyer ON r.BuyerId = buyer.UserId
LEFT JOIN [User] seller ON r.SellerId = seller.UserId
LEFT JOIN [User] staff ON c.Thirdparty = staff.UserId 
WHERE c.IsDelete = 0 ";
                using var connection = CreateConnection();
                return await connection.QueryAsync<dynamic>(query);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        public async Task<IEnumerable<dynamic>> GetAllContractForSellerAsync(string userId)
        {
            try
            {
                var query = @"
SELECT  c.ContractId,
        r.ReservationId,
        r.BuyerId,
        r.SellerId,
        r.PropertyId,
        p.PropertyTitle,
        c.Thirdparty,
        c.ContractStatus,
        c.Deposit,
        buyer.Firstname AS BuyerFirstName,
        buyer.Lastname AS BuyerLastName,
        seller.Firstname AS SellerFirstName,
        seller.Lastname AS SellerLastName,
        staff.Firstname AS StaffFirstName,
        staff.Lastname AS StaffLastName
FROM [Contract] c
INNER JOIN Reservation r ON c.ReservationId = r.ReservationId
INNER JOIN Property p ON r.PropertyId = p.PropertyId
LEFT JOIN [User] buyer ON r.BuyerId = buyer.UserId
LEFT JOIN [User] seller ON r.SellerId = seller.UserId
LEFT JOIN [User] staff ON c.Thirdparty = staff.UserId
WHERE r.SellerId = @sellerIdValue AND c.IsDelete = 0 ";

                var parameter = new DynamicParameters();
                parameter.Add("sellerIdValue", userId);
                using var connection = CreateConnection();
                return await connection.QueryAsync<dynamic>(query,parameter);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        public async Task<IEnumerable<dynamic>> GetAllContractForBuyerAsync(string userId)
        {
            try
            {
                var query = @"
SELECT  c.ContractId,
        r.ReservationId,
        r.BuyerId,
        r.SellerId,
        r.PropertyId,
        p.PropertyTitle,
        c.Thirdparty,
        c.ContractStatus,
        c.Deposit,
        buyer.Firstname AS BuyerFirstName,
        buyer.Lastname AS BuyerLastName,
        seller.Firstname AS SellerFirstName,
        seller.Lastname AS SellerLastName,
        staff.Firstname AS StaffFirstName,
        staff.Lastname AS StaffLastName
FROM [Contract] c
INNER JOIN Reservation r ON c.ReservationId = r.ReservationId
INNER JOIN Property p ON r.PropertyId = p.PropertyId
LEFT JOIN [User] buyer ON r.BuyerId = buyer.UserId
LEFT JOIN [User] seller ON r.SellerId = seller.UserId
LEFT JOIN [User] staff ON c.Thirdparty = staff.UserId
WHERE r.BuyerId = @buyerIdValue AND c.IsDelete = 0 ";

                var parameter = new DynamicParameters();
                parameter.Add("buyerIdValue", userId);
                using var connection = CreateConnection();
                return await connection.QueryAsync<dynamic>(query, parameter);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        public async Task<int> UpdateContractStatusAsync(string contractId, string status)
        {
            try
            {
                var query = "UPDATE [Contract] " +
                    "SET ContractStatus = @contractStatusValue " +
                    "WHERE ContractId = @contractIdValue ";
                var parameter = new DynamicParameters();
                parameter.Add("contractStatusValue", status);
                parameter.Add("contractIdValue", contractId);
                using var connection = CreateConnection();
                return await connection.ExecuteAsync(query, parameter);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        public async Task<dynamic> GetContractDetailAsync(string contractId)
        {
            try
            {
                var query = @"
SELECT  c.ContractId,
        r.ReservationId,
        r.BuyerId,
        r.SellerId,
        r.PropertyId,
        p.PropertyTitle,
        c.Thirdparty,
        c.ContractStatus,
        c.ContractScript,
        c.Deposit,
        p.Price,
        buyer.Firstname AS BuyerFirstName,
        buyer.Lastname AS BuyerLastName,
        seller.Firstname AS SellerFirstName,
        seller.Lastname AS SellerLastName,
        staff.Firstname AS StaffFirstName,
        staff.Lastname AS StaffLastName
FROM [Contract] c
INNER JOIN Reservation r ON c.ReservationId = r.ReservationId
INNER JOIN Property p ON r.PropertyId = p.PropertyId
LEFT JOIN [User] buyer ON r.BuyerId = buyer.UserId
LEFT JOIN [User] seller ON r.SellerId = seller.UserId
LEFT JOIN [User] staff ON c.Thirdparty = staff.UserId 
WHERE c.IsDelete = 0 AND c.ContractId = @contractIdValue ";
                var parameter = new DynamicParameters();
                parameter.Add("contractIdValue", contractId);
                using var connection = CreateConnection();
                return await connection.QuerySingleOrDefaultAsync<dynamic>(query, parameter);
            
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        public async Task<int> AddContractAsync(string contractId,string contract, string userId)
        {
            try
            {
                var query = @"
UPDATE [dbo].[Contract]
   SET 
      [Thirdparty] = @userIdValue     
      ,[ContractScript] = @contractValue
 WHERE ContractID = @contractIdValue
";
                var parameter = new DynamicParameters();
                parameter.Add("contractValue", contract);
                parameter.Add("contractIdValue", contractId);
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
