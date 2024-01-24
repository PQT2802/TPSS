using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPSS.Data.Helper;
using System.Data
using TPSS.Data.Models.Entities;

namespace TPSS.Data.Repository.Impl
{
    internal class ReservationRepository : BaseRepository, IReservationRepository
    {
        public ReservationRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<int> CreateReservationAsync(Reservation newReservation)
        {
            try
            {
                var query = "INSERT INTO [Reservation] (ReservationId, SellerId, BuyerId, PropertyId, BookingDate)"
                          + "VALUE (@ReservationId, @SellerId, @BuyerId, @PropertyId, @BookingDate)";
                var parameter = new DynamicParameters();
                parameter.Add("ReservationId",newReservation.ReservationId, DbType.String);
                parameter.Add("SellerId", newReservation.SellerId, DbType.String);
                parameter.Add("BuyerId", newReservation.BuyerId, DbType.String);
                parameter.Add("PropertyId", newReservation.PropertyId, DbType.String);
                parameter.Add("BookingDate", newReservation.BookingDate, DbType.Date);
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
                    "SET IsDelete = true" +
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

        public async Task<Reservation> GetReservationByIdAsync(string reservationId)
        {
            try
            {
                var query = "SELECT *" +
                    "FROM [ReservationId]" +
                    "WHERE ReservationId = @ReservationId";
                var parameter = new DynamicParameters();
                parameter.Add("ReservationId", reservationId, DbType.String);
                using var connection = CreateConnection();
                return await connection.QuerySingleAsync<Reservation>(query, parameter);
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
    }
}
