using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;
using TPSS.Business.Common;
using TPSS.Business.Service;
using TPSS.Data.Models.DTO;
using TPSS.Data.Models.Entities;

namespace TPSS.API.Controllers
{
    [ApiController]
    [Route("api/v1.0/Contract")]
    [EnableCors]
    public class ContractController : ControllerBase
    {
        private readonly IContractService  _contractService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ContractController(IContractService contractService, IHttpContextAccessor httpContextAccessor)
        {
            _contractService = contractService;
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpPost("CreateContract")]
        public async Task<IActionResult> CreateContractAsync(string reservationId)
        {
            var result = await _contractService.CreateContractAsync(reservationId);
            return Ok(result);
        }
        [HttpGet("GetAllContract")]
        public async Task<IActionResult> GetAllContractAsync()
        {
            var result = await _contractService.GetAllContractAsync();
            return Ok(result);
        }
        [HttpGet("GetAllContractForSeller")]
        public async Task<IActionResult> GetAllContractForSellerAsync()
        {
            CurrentUserObject c = await TokenHepler.Instance.GetThisUserInfo(HttpContext);
            var result = await _contractService.GetAllContractForSellerAsync(c.UserId);
            return Ok(result);
        }
        [HttpGet("GetAllContractForBuyer")]
        public async Task<IActionResult> GetAllContractForBuyerAsync()
        {
            CurrentUserObject c = await TokenHepler.Instance.GetThisUserInfo(HttpContext);
            var result = await _contractService.GetAllContractForBuyerAsync(c.UserId);
            return Ok(result);
        }
        [HttpGet("UpdateContractStatus")]
        public async Task<IActionResult> UpdateContractStatusAsync(string contractId, string status)
        {
            var result = await _contractService.UpdateContractStatusAsync(contractId,status);
            return Ok(result);
        }
        [HttpGet("GetContractDetail")]
        public async Task<IActionResult> GetContractDetailAsync(string contractId)
        {
            var result = await _contractService.GetContractDetailAsync(contractId);
            return Ok(result);
        }
        [HttpPost("AddContract")]
        public async Task<IActionResult> AddContractAsync(string contractId, string contract)
        {
            CurrentUserObject c = await TokenHepler.Instance.GetThisUserInfo(HttpContext);
            var result = await _contractService.AddContractAsync(contractId,contract,c.UserId);
            return Ok(result);
        }
    }
}
