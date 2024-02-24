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
                p.Image,
                p.PropertyTitle,
                p.PropertyID,
                pd.OwnerID,
                owner.Firstname AS OwnerFirstname,
                owner.Lastname AS OwnerLastname,
                r.Status,
                r.BookingDate
            FROM Reservation r
            INNER JOIN [User] buyer ON r.BuyerID = buyer.UserId
            INNER JOIN Property p ON r.PropertyID = p.PropertyID
            INNER JOIN [dbo].[PropertyDetail] pd ON p.PropertyID = pd.PropertyID
            INNER JOIN [User] owner ON pd.OwnerID = owner.UserId
            WHERE buyer.UserId = @userId";

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
        public async Task<IEnumerable<dynamic>> GetReservationForSellerAsync(string userId)
        {
            try
            {
                var query = @"
            SELECT
                p.Image,
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
            WHERE r.SellerID = @userId";

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
    }
}
