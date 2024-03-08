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
    public class ReservationRepository : BaseRepository, IReservationRepository
    {
        public ReservationRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<int> CreateReservationAsync(Reservation reservation)
        {
            try
            {
                var query = "INSERT INTO [Reservation] (ReservationId, SellerId, BuyerId, PropertyId, BookingDate, Status, Priority, IsDelete) " +
                    "VALUES ( " +
                    "@ReservationIdValue, " +
                    "@SellerIdValue, " +
                    "@BuyerIdValue, " +
                    "@PropertyIdValue, " +
                    "@BookingDateValue, " +
                    "@StatusValue, " +
                    "@PriorityValue, " +
                    "@IsDeleteValue )";
                var parameter = new DynamicParameters();
                parameter.Add("ReservationIdValue", reservation.ReservationId, DbType.String);
                parameter.Add("SellerIdValue", reservation.SellerId, DbType.String);
                parameter.Add("BuyerIdValue", reservation.BuyerId, DbType.String);
                parameter.Add("PropertyIdValue", reservation.PropertyId, DbType.String);
                parameter.Add("BookingDateValue", reservation.BookingDate, DbType.DateTime);
                parameter.Add("StatusValue", reservation.Status, DbType.String);
                parameter.Add("PriorityValue", reservation.Priority, DbType.Int16);
                parameter.Add("IsDeleteValue", reservation.IsDelete, DbType.Boolean);
                using var connection = CreateConnection();
                return await connection.ExecuteAsync(query, parameter);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        public async Task<Reservation> GetReservation(string userId, string properetyId)
        {
            try
            {
                var query = "SELECT * " +
                    "FROM Reservation " +
                    "WHERE BuyerId = @userIdValue AND PropertyId = @propertyIdValue";
                var parameter = new DynamicParameters();
                parameter.Add("userIdValue", userId);
                parameter.Add("propertyIdValue", properetyId);
                using var connection = CreateConnection();
                return await connection.QuerySingleOrDefaultAsync<Reservation>(query,parameter);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<string> GetColumnData(string columnName, string baseOnData)
        {
            try
            {
                var query = "";
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<string> GetLatestReservationIdAsync()
        {
            try
            {
                var query = "SELECT TOP 1 ReservationID " +
    "FROM [Reservation] " +
    "ORDER BY " +
    "CAST(SUBSTRING(ReservationID, 8, LEN(ReservationID)) AS INT) DESC, " +
    "ReservationID DESC";
                using var connection = CreateConnection();
                return await connection.QuerySingleOrDefaultAsync<string>(query);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message, e);
            }
        }
        public async Task<IEnumerable<dynamic>> GetReservationForBuyerAsync(string userId)
        {
            try
            {
                var query = @"
        SELECT
            r.ReservationId
            p.PropertyTitle,
            p.PropertyID,
            pd.OwnerID,
            owner.Firstname AS OwnerFirstname,
            owner.Lastname AS OwnerLastname,
            r.Status,
            r.BookingDate,
            a.Image
        FROM Reservation r
        INNER JOIN [User] buyer ON r.BuyerID = buyer.UserId
        INNER JOIN Property p ON r.PropertyID = p.PropertyID
        INNER JOIN [dbo].[PropertyDetail] pd ON p.PropertyID = pd.PropertyID
        INNER JOIN [User] owner ON pd.OwnerID = owner.UserId
        LEFT JOIN Album a ON p.PropertyID = a.PropertyID AND a.ImageDescription = 'Homepage'
        WHERE buyer.UserId = @userId AND r.IsDelete = 0 ";

                var parameter = new DynamicParameters();
                parameter.Add("userId", userId, DbType.String);

                using var connection = CreateConnection();
                return await connection.QueryAsync<dynamic>(query, parameter);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        public async Task<IEnumerable<dynamic>> GetReservationForSellerAsync(string userId, string propertyId)
        {
            try
            {
                var query = @"
            SELECT
                r.ReservationId
                a.Image,
                p.PropertyTitle,
                p.PropertyId,
                buyer.UserId AS BuyerId,
                buyer.Firstname AS BuyerFirstname,
                buyer.Lastname AS BuyerLastname,
                r.Status,
                r.BookingDate
            FROM Reservation r
            INNER JOIN [User] seller ON r.SellerId = seller.UserId
            INNER JOIN Property p ON r.PropertyId = p.PropertyId
            INNER JOIN [User] buyer ON r.BuyerId = buyer.UserId
            LEFT JOIN Album a ON p.PropertyID = a.PropertyID AND a.ImageDescription = 'Homepage'
            WHERE r.SellerID = @userId AND r.PropertyId = @PropertyIdValue AND r.IsDelete = 0 ";

                var parameter = new DynamicParameters();
                parameter.Add("userId", userId, DbType.String);
                parameter.Add("PropertyIdValue", propertyId, DbType.String);

                using var connection = CreateConnection();
                return await connection.QueryAsync<dynamic>(query, parameter);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        public async Task<int> DeleteReservation(string reservationId)
        {
            try
            {
                var query = @"
UPDATE Reservation
SET IsDelete = 1
WHERE ReservationId = @reservationIdValue
";
                var parameter = new DynamicParameters();
                parameter.Add("reservationIdValue", reservationId);
                using var connection = CreateConnection();
                return await connection.ExecuteAsync(query, parameter);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        public async Task<int> AccpectReservation(string reservationId)
        {
            try
            {
                var query = @"
UPDATE Reservation 
SET Status = 'Accepted'
WHERE ReservationId = @reservationIdValue AND IsDelete = 0
";
                var parameter = new DynamicParameters();
                parameter.Add("reservationIdValue", reservationId);
                using var connection = CreateConnection();
                return await connection.ExecuteAsync(query, parameter);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message, e);
            }
        }
        public async Task<int> RejectReservation(string reservationId)
        {
            try
            {
                var query = @"
UPDATE Reservation 
SET Status = 'Rejected'
WHERE ReservationId = @reservationIdValue AND IsDelete = 0
";
                var parameter = new DynamicParameters();
                parameter.Add("reservationIdValue", reservationId);
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
