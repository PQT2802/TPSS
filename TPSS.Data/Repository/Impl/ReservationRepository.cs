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

    }
}
