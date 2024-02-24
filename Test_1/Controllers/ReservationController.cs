using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TPSS.Business.Common;
using TPSS.Business.Service;
using TPSS.Data.Models.DTO;

namespace TPSS.API.Controllers
{
    [ApiController]
    [Route("api/v1.0/Reservation")]
    [EnableCors]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ReservationController (IReservationService reservationService, IHttpContextAccessor httpContextAccessor )
        {
            _reservationService = reservationService;
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpPost]
        public async Task<IActionResult> CreateReservationAsync(string propertyId)
        {
            CurrentUserObject c = await TokenHepler.Instance.GetThisUserInfo(HttpContext);
            var result = await _reservationService.CreateReservationAsynce(c.UserId, propertyId);
            return Ok(result);
        }
        [HttpGet("GetForBuyer")]
        public async Task<IActionResult> GetReservationForBuyerAsync()
        {
            CurrentUserObject c = await TokenHepler.Instance.GetThisUserInfo(HttpContext);
            var result = await _reservationService.GetReservationForBuyerAsync(c.UserId);
            return Ok(result);
        }
        [HttpGet("GetForSeller")]
        public async Task<IActionResult> GetReservationForSellerrAsync(string propertyId)
        {
            CurrentUserObject c = await TokenHepler.Instance.GetThisUserInfo(HttpContext);
            var result = await _reservationService.GetReservationForSellerAsync(c.UserId, propertyId);
            return Ok(result);
        }
    }
}
