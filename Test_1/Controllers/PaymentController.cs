using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TPSS.Business.Common;
using TPSS.Business.Service;
using TPSS.Business.Service.Impl;
using TPSS.Data.Models.DTO;

namespace TPSS.API.Controllers
{
    [ApiController]
    [Route("api/v1.0/Payment")]
    [EnableCors]
    public class PaymentController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPaymentService _paymentService;
        public PaymentController(IHttpContextAccessor httpContextAccessor, IPaymentService paymentService)
        {
            _httpContextAccessor = httpContextAccessor;
            _paymentService = paymentService;
        }
        [HttpPost("CreatePayment")]
        public async Task<IActionResult> CreatePaymentAsync(string contractId)
        {
            var result = await _paymentService.CreatePaymentAsync(contractId);
            return Ok(result);
        }
        [HttpGet("GetPaymentForSeller")]
        public async Task<IActionResult> GetPaymentForSellerAsync(string contractId, string userId)
        {

            CurrentUserObject c = await TokenHepler.Instance.GetThisUserInfo(HttpContext);
            var result = await _paymentService.GetPaymentForSellerAsync(contractId,c.UserId);
            return Ok(result);
        }
        [HttpGet("GetPaymentForBuyer")]
        public async Task<IActionResult> GetPaymentForBuyerAsync(string contractId, string userId)
        {

            CurrentUserObject c = await TokenHepler.Instance.GetThisUserInfo(HttpContext);
            var result = await _paymentService.GetPaymentForBuyerAsync(contractId, c.UserId);
            return Ok(result);
        }
        [HttpGet("GetPaymentDetail")]
        public async Task<IActionResult> GetPaymentDetailAsync(string paymentId)
        {
            var result = await _paymentService.GetPaymentDetailAsync(paymentId);
            return Ok(result);
        }
    }
}
