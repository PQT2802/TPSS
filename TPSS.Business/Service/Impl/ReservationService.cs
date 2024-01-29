using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPSS.Data.Models.DTO;
using TPSS.Data.Models.Entities;
using TPSS.Data.Repository;
using TPSS.Data.Repository.Impl;

namespace TPSS.Business.Service.Impl
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository  _reservationRepository;

        public ReservationService(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }


        public async Task<int> CreateReservationAsync(ReservationDTO newReservation)
        {
            try
            {
                Reservation reservation = new()
                {
                    ReservationId = await AutoGenerateReservationId(),
                    BuyerId = newReservation.BuyerId,//lay tu request param
                    PropertyId = newReservation.PropertyId,//lay propertyId join propertydetail, lay ownnerid = seller id
                    SellerId = GetOwnerIdByPropertyId(newReservation.PropertyId) ,//xoa, lay propertyId join propertydetail, lay ownnerid = seller id
                    BookingDate =ContractService.GetTodaysDate(),
                    Status = false,
                    Priority = await GetNumberOfPriority(newReservation.PropertyId),
                    IsDelete = false,
                };
                int result = await _reservationRepository.CreateReservationAsync(reservation);
                return result;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message, e);
            }
        }

        private async Task<string> AutoGenerateReservationId()
        {
            string newReservationid;
            string latestReservationId = await _reservationRepository.GetLatestReservationIdAsync();
            if (latestReservationId != null)
            {
                int numericpart = int.Parse(latestReservationId[2..]);
                int newnumericpart = numericpart + 1;
                newReservationid = $"RE{newnumericpart:d8}";
            }
            else
            {
                newReservationid = "RE00000001";
            }
            return newReservationid;
        }
        public async Task<int> GetNumberOfPriority(String? propertyId)
        {
            try
            {
                int result = await _reservationRepository.GetNumberOfPriority(propertyId);
                result += 1;
                return result;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message, e);
            }
        }

        public String GetOwnerIdByPropertyId(String? propertyId)//lay sellerid
        {
            try
            {
                String result =  _reservationRepository.GetOwnerIdByPropertyId(propertyId).Result;
                return result;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message, e);
            }
        }

        public async Task<int> DeleteReservationAsync(string id)
        {
            try
            {
                int result = await _reservationRepository.DeleteReservationAsync(id);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task<Reservation> GetReservationByIdAsync(string id)//lay all
        {
            try
            {
                Reservation result = await _reservationRepository.GetReservationByIdAsync(id);
                return result;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message, e);
            }
        }

        public async Task<DateOnly> GetdateReservationByIdAsync(string id) //lay date
        {
            try
            {
                DateOnly result = await _reservationRepository.GetDateReservationByIdAsync(id);
                return result;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message, e);
            }
        }

        public async Task<int> UpdateReservationAsync(ReservationDTO updateReservation)
        {
            try
            {
                Reservation Reservation = new()
                {
                    SellerId = updateReservation.SellerId,
                    BuyerId = updateReservation.BuyerId,
                    PropertyId = updateReservation.PropertyId,
                    BookingDate = updateReservation.BookingDate,
                    Status = updateReservation.Status,
                    Priority = updateReservation.Priority,
                };
                int result = await _reservationRepository.UpdateReservationAsync(Reservation);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
    }
}
