using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPSS.Data.Models.DTO;

namespace TPSS.Business.Service.Impl
{
    public class ReservationService : IReservationService
    {
        public async Task<int> CreateReservationAsynce(ReservationDTO reservationDTO)
        {
			try
			{
                return 0;
			}
			catch (Exception e )
			{

                throw new Exception(e.Message, e);
            }
        }
    }
}
