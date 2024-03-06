using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPSS.Data.Helper;
using TPSS.Data.Models.Entities;

namespace TPSS.Data.Repository.Impl
{
    public class PaymentRepository : BaseRepository, IPaymentRepository
    {
        public PaymentRepository(IConfiguration configuration) : base(configuration)
        {
        }
        public async Task<int> CreatePaymentAsync(Payment newPayment)
        {
            try
            {
                var query = @"
INSERT INTO [Payment](PaymentId,ContractId,Status,CommissionCalculation,IsDelete,Amount)
VALUES (PaymentId = @paymentIdValue, ContractId = @contractIdValue, Status = 'Processing',CommissionCalculation = @commissionCalculationValue,Amount =@ amountValue
";
                var parameter = new DynamicParameters();
                parameter.Add("paymentIdValue",newPayment.PaymentId );
                parameter.Add("contractIdValue", newPayment.ContractId);
                parameter.Add("commissionCalculationValue", newPayment.CommissionCalculation);
                parameter.Add("amountValue", newPayment.Amount);
                using var connection = CreateConnection();
                return await connection.ExecuteAsync(query, parameter);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        public async Task<IEnumerable<dynamic>> GetPaymentForSellerAsync(string contractId, string userId)
        {
            try
            {
                var query = @"
SELECT  c.ContractId,
        pm.PaymentId,
        r.BuyerId,
        r.SellerId,
        r.PropertyId,
        c.Thirdparty,
        pm.Amount,
        pm.CommissionCalculation,
        buyer.Firstname AS BuyerFirstName,
        buyer.Lastname AS BuyerLastName,
        seller.Firstname AS SellerFirstName,
        seller.Lastname AS SellerLastName,
        staff.Firstname AS StaffFirstName,
        staff.Lastname AS StaffLastName
FROM [Payment] pm
INNER JOIN [Contract] c ON pm.ContractId = c.ContractId
INNER JOIN Reservation r ON c.ReservationId = r.ReservationId
INNER JOIN Property p ON r.PropertyId = p.PropertyId
LEFT JOIN [User] buyer ON r.BuyerId = buyer.UserId
LEFT JOIN [User] seller ON r.SellerId = seller.UserId
LEFT JOIN [User] staff ON c.Thirdparty = staff.UserId
WHERE r.SellerId = @sellerIdValue AND pm.ContractId = @contractIdValue";
                var parameter = new DynamicParameters();
                parameter.Add("sellerIdValue", userId);
                parameter.Add("contractIdValue", contractId);
                using var connection = CreateConnection();
                return await connection.QueryAsync<dynamic>(query, parameter);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        public async Task<IEnumerable<dynamic>> GetPaymentForBuyerAsync(string contractId, string userId)
        {
            try
            {
                var query = @"
SELECT  c.ContractId,
        pm.PaymentId,
        r.BuyerId,
        r.SellerId,
        r.PropertyId,
        c.Thirdparty,
        pm.Amount,
        pm.CommissionCalculation,
        buyer.Firstname AS BuyerFirstName,
        buyer.Lastname AS BuyerLastName,
        seller.Firstname AS SellerFirstName,
        seller.Lastname AS SellerLastName,
        staff.Firstname AS StaffFirstName,
        staff.Lastname AS StaffLastName
FROM [Payment] pm
INNER JOIN [Contract] c ON pm.ContractId = c.ContractId
INNER JOIN Reservation r ON c.ReservationId = r.ReservationId
INNER JOIN Property p ON r.PropertyId = p.PropertyId
LEFT JOIN [User] buyer ON r.BuyerId = buyer.UserId
LEFT JOIN [User] seller ON r.SellerId = seller.UserId
LEFT JOIN [User] staff ON c.Thirdparty = staff.UserId
WHERE r.BuyerId = @buyerIdValue AND pm.ContractId = @contractIdValue";
                var parameter = new DynamicParameters();
                parameter.Add("buyerIdValue", userId);
                parameter.Add("contractIdValue", contractId);
                using var connection = CreateConnection();
                return await connection.QueryAsync<dynamic>(query, parameter);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        public async Task<dynamic> GetPaymentDetailAsync(string paymentId)
        {
            var query = @"
SELECT  c.ContractId,
        pm.PaymentId,
        r.BuyerId,
        r.SellerId,
        r.PropertyId,
        c.Thirdparty,
        pm.Amount,
        pm.CommissionCalculation,
        buyer.Firstname AS BuyerFirstName,
        buyer.Lastname AS BuyerLastName,
        seller.Firstname AS SellerFirstName,
        seller.Lastname AS SellerLastName,
        staff.Firstname AS StaffFirstName,
        staff.Lastname AS StaffLastName
FROM [Payment] pm
INNER JOIN [Contract] c ON pm.ContractId = c.ContractId
INNER JOIN Reservation r ON c.ReservationId = r.ReservationId
INNER JOIN Property p ON r.PropertyId = p.PropertyId
LEFT JOIN [User] buyer ON r.BuyerId = buyer.UserId
LEFT JOIN [User] seller ON r.SellerId = seller.UserId
LEFT JOIN [User] staff ON c.Thirdparty = staff.UserId
WHERE pm.PaymentId = @paymentIdValue ";
            var parameter = new DynamicParameters();
            parameter.Add("paymentIdValue", paymentId);
            using var connection = CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<dynamic>(query, parameter);
        }

        public async Task<string> GetLatestPaymentIdAsync()
        {
            try
            {
                var query = "SELECT TOP 1 PaymentId " +
    "FROM [Payment] " +
    "ORDER BY " +
    "CAST(SUBSTRING(PaymentId, 8, LEN(PaymentId)) AS INT) DESC, " +
    "PaymentId DESC";
                using var connection = CreateConnection();
                return await connection.QuerySingleOrDefaultAsync<string>(query);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message, e);
            }
        }
    }
}
