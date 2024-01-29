using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TPSS.Business.Service;
using TPSS.Business.Service.Impl;
using TPSS.Data.Models.DTO;

namespace TPSS.API.Controllers
{
    [ApiController]
    [Route("api/v1.0/contract")]
    [EnableCors]
    public class ContractController : Controller
    {
        public readonly IContractService _contractService;

        public ContractController(IContractService contractService) 
        {
            _contractService = contractService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateContractAsync(ContractDTO newContract)
        {
            var result = await _contractService.CreateContractAsync(newContract);
            return Ok(result);
        }



        [HttpDelete]
        public async Task<IActionResult> DeleteContractAsync(String id)
        {
            var result = await _contractService.DeleteContractAsync(id);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetContractByIdAsync(String id)
        {
            var result = await _contractService.GetContractByIdAsync(id);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetdateContractByIdAsync(String id)//test
        {
            var result = await _contractService.GetdateContractByIdAsync(id);
            return Ok(result);
        }
    }
}
