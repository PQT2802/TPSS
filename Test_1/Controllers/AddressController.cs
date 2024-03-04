using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TPSS.Business.Service;
using TPSS.Business.Service.Impl;
using TPSS.Data.Models.DTO;

namespace TPSS.API.Controllers
{
    [ApiController]
    [Route("api/v1.0/Address")]
    [EnableCors]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;
        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }
        [HttpGet("GetCity")]
        public async Task<IActionResult> GetCity()
        {
            var result = await _addressService.GetCity();
            return Ok(result);
        }
        [HttpGet("GetDistrict")]
        public async Task<IActionResult> GetDistrict(string addressId)
        {
            var result = await _addressService.GetDistrict(addressId);
            return Ok(result);
        }
        [HttpGet("GetWard")]
        public async Task<IActionResult> GetWard(string addressId, string district)
        {
            var result = await _addressService.GetWard(addressId, district);
            return Ok(result);
        }
    }
}
