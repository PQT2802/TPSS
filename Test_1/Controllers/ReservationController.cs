using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TPSS.Business.Service;
using TPSS.Business.Service.Impl;
using TPSS.Data.Models.DTO;

namespace TPSS.API.Controllers
{
    [ApiController]
    [Route("api/v1.0/reservation")]
    [EnableCors]
    public class ReservationController : Controller
    {
        private readonly IReservationService _reservationService;
        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateReservationAsync(ReservationDTO newReservation)
        {
            var result = await _reservationService.CreateReservationAsync(newReservation);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteReservationAsync(String id)
        {
            var result = await _reservationService.DeleteReservationAsync(id);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetReservationByIdAsync(String id)
        {
            var result = await _reservationService.GetReservationByIdAsync(id);
            return Ok(result);
        }
        [HttpGet ("{id}")]
        public async Task<IActionResult> GetdateReservationByIdAsync(String id)//test
        {
            var result = await _reservationService.GetdateReservationByIdAsync(id);
            return Ok(result);
        }


    }
}
