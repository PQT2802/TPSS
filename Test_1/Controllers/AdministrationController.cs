using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TPSS.Business.Service;
using TPSS.Data.Models.DTO;

namespace TPSS.API.Controllers
{
    [ApiController]
    [Route("api/v1.0/Administration")]
    [EnableCors]
    public class AdministrationController : ControllerBase
    {
        private readonly IUserService _userService;

        public AdministrationController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPut("UpdateUserRole")]
        public async Task<IActionResult> UpdateUserRole(string userId, string roleId)
        {
            var result = await _userService.UpdateUserRole(userId, roleId);
            return Ok(result);
        }
        [HttpDelete("DeleteAccount")]
        public async Task<IActionResult> DeleteUserAsynce(string userId)
        {
            var result = await _userService.DeleteUserAsync(userId);
            return Ok(result);
        }
    }
}
