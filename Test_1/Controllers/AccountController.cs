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
        public async Task<IActionResult> RegisterUserAsync(RegisterDTO registerDTO, string confirmCode)
        {
            var result = await _userService.RegistUserAsync(registerDTO, confirmCode);
            return Ok(result);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> GetUserAcountAsync(LoginDTO loginDTO)
        {
            var result = await _userService.GetUserAccountAsync(loginDTO);
            return Ok(result);
        }
        [HttpPost("LoginWithGoogle")]
        public async Task<IActionResult> GetTokenFirebase(string firebaseToken)
        {
            var result = await _userService.GetTokenFirebase(firebaseToken);
            return Ok(result);
           //return ve giong login 
        }
        [HttpPost("SendConfirmEmailCode")]
        public async Task<IActionResult> SendConfirmationEmail(string toEmailAddress)
        {
            await _userService.SendConfirmationEmail(toEmailAddress);
            return Ok("Verify code is sending to your email");
        }
        [HttpPut("ChangingPassword")]
        public async Task<IActionResult> UpdatePasswordAsync(string userId, ChangingPasswordDTO changingPasswordDTO)
        {
            var result = await _userService.UpdatePasswordAsync(userId, changingPasswordDTO);
            return Ok(result);
            //return ve errors neu co loi
            // return 1 neu thang cong
        }

    }
}
