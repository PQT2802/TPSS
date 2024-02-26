using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TPSS.Business.Common;
using TPSS.Business.Service;
using TPSS.Data.Models.DTO;
using TPSS.Data.Models.Entities;

namespace TPSS.API.Controllers
{
    [ApiController]
    [Route("api/v1.0/users")]
    [EnableCors]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        
        public UserController(IUserService userService, IHttpContextAccessor httpContextAccessor )
        {
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
        }
 
        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteUserAsync(String id)
        {
            var result = await _userService.DeleteUserAsync(id);
            return Ok(result);
        }


        [HttpGet]
       
        public async Task<IActionResult> GetUserdAsync()
        {
            CurrentUserObject c = await TokenHepler.Instance.GetThisUserInfo(HttpContext);
            var result = await _userService.GetInforUserAsync(c.UserId);
            return Ok(result);
        }



    }

}

