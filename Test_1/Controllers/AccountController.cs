using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TPSS.Business.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using TPSS.Business.Service;
using TPSS.Data.Models.DTO;

namespace TPSS.API.Controllers
{
    [ApiController]
    [Route("api/v1.0/Account")]
    [EnableCors]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Regist")]
        public async Task<IActionResult> RegisterUserAsync(RegisterDTO registerDTO)
        {
            var result = await _userService.RegistUserAsync(registerDTO);
            return Ok(result);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> GetUserAcountAsync(LoginDTO loginDTO)
        {
            var result = await _userService.GetUserAccountAsync(loginDTO);
            return Ok(result);
        }


    }
}
