using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPSS.Data.Helper;
using System.Data;
using TPSS.Data.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TPSS.Data.Models.DTO;

namespace TPSS.Data.Repository.Impl
{
    public class ReservationRepository : BaseRepository, IReservationRepository
    {
        public ReservationRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<int> CreateReservationAsync(Reservation newReservation)
        {
            try
            {
                var query = "INSERT INTO [Reservation] (ReservationId, SellerId, BuyerId, PropertyId, BookingDate, Status, Priority, IsDelete)"
                          + "VALUES (@ReservationId, @SellerId, @BuyerId, @PropertyId, @BookingDate, @Status, @Priority, @IsDelete)";
                var parameter = new DynamicParameters();
                parameter.Add("ReservationId",newReservation.ReservationId, DbType.String);
                parameter.Add("SellerId", newReservation.SellerId, DbType.String);
                parameter.Add("BuyerId", newReservation.BuyerId, DbType.String);
                parameter.Add("PropertyId", newReservation.PropertyId, DbType.String);
                parameter.Add("BookingDate", newReservation.BookingDate, DbType.Date);
                parameter.Add("Status", newReservation.Status, DbType.Boolean);
                parameter.Add("Priority", newReservation.Priority, DbType.Int64);
                parameter.Add("IsDelete", newReservation.IsDelete, DbType.Boolean);
                using var connection = CreateConnection();
                return await connection.ExecuteAsync(query, parameter);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task<int> DeleteReservationAsync(string reservationId)
        {
            try
            {
                var query = "UPDATE [Reservation] " +
                    "SET IsDelete = 1" +
                    "WHERE ReservationId = @ReservationId";
                var parameter = new DynamicParameters();
                parameter.Add("ReservationId", reservationId, DbType.String);
                using var connection = CreateConnection();
                return await connection.ExecuteAsync(query, parameter);

            }
            catch (Exception e)
            {

                throw new Exception(e.Message, e);
            }
        }

        public async Task<Reservation> GetReservationByIdAsync(string reservationId)//this still error
        {
            try
            {
                var query = "SELECT ReservationId, SellerId, BuyerId, ProperyId, Status, Priority, isDelelte " +
                    "FROM Reservation " +
                    "WHERE ReservationId = @ReservationId";
                var parameter = new DynamicParameters();
                parameter.Add("ReservationId", reservationId, DbType.String);
                using var connection = CreateConnection();
                Reservation reservation = await connection.QuerySingleAsync<Reservation>(query, parameter);
                return reservation;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task<DateOnly> GetDateReservationByIdAsync(string reservationId) //kiem tra ngay
        {
            try
            {
                var query = "SELECT BookingDate " +
                    "FROM Reservation " +
                    "WHERE ReservationId = @ReservationId";
                var parameter = new DynamicParameters();
                parameter.Add("ReservationId", reservationId, DbType.String);
                using var connection = CreateConnection();
                DateTime date =  await connection.QuerySingleAsync<DateTime>(query, parameter);
                DateOnly dateOnly = DateOnly.FromDateTime(date);
                return dateOnly;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task<int> UpdateReservationAsync(Reservation updateReservation)
        {
            try
            {
                var query = "UPDATE [Reservation]" +
                    "SET SellerId = @SellerId, BuyerId = @BuyerId, PropertyId = @PropertyId, BookingDate = @BookingDate" +
                    "FROM [Reservation]"+
                    "WHERE ReservationId = @ReservationId";

                var parameter = new DynamicParameters();
                parameter.Add("SellerId", updateReservation.SellerId, DbType.String);
                parameter.Add("BuyerId", updateReservation.BuyerId, DbType.String);
                parameter.Add("PropertyId", updateReservation.PropertyId, DbType.String);
                parameter.Add("BookingDate", updateReservation.BookingDate, DbType.Date);
                parameter.Add("ReservationId", updateReservation.ReservationId, DbType.String);
                using var connection = CreateConnection();
                return await connection.ExecuteAsync(query, parameter);
            }catch (Exception e)
            { 
                throw new Exception(e.Message,e); 
            }
        }
        public async Task<string> GetLatestReservationIdAsync()
        {
            try
            {
                var query = "SELECT TOP 1 ReservationId " +
                    "FROM [Reservation] " +
                    "ORDER BY " +
                    "CAST(SUBSTRING(ReservationId, 8, LEN(ReservationId)) AS INT) DESC, " +
                    "ReservationId DESC";
                using var connection = CreateConnection();
                return await connection.QueryFirstOrDefaultAsync<string>(query);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message, e);
            }
        }
        
        public async Task<int> GetNumberOfPriority(String? propertyId)//return 0 if null
        {
            try
            {
                var query = "SELECT COUNT(*)" +
                            "FROM [Reservation]" +
                            "WHERE PropertyId = @PropertyId";
                using var connection = CreateConnection();
                var parameter = new DynamicParameters();
                parameter.Add("PropertyId", propertyId, DbType.String);
                var result = await connection.QueryFirstAsync<int>(query, parameter);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task<String> GetOwnerIdByPropertyId(String? propertyId)
        {
            try
            {
                var query = "SELECT OwnerId" +
                            "FROM [PropertyDetail]" +
                            "WHERE PropertyId = @PropertyId";
                using var connection = CreateConnection();
                var parameter = new DynamicParameters();
                parameter.Add("PropertyId", propertyId, DbType.String);
                var result = await connection.QuerySingleAsync<String>(query, parameter);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
    }
}
