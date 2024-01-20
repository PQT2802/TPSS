using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]        
        public async Task<IActionResult> CreateUserAsync(UserDTO newUser)
        {
            var result = await _userService.CreateUserAsync(newUser);
            return Ok(result);//tra ve respone(status:200,body:result)
        }

        [HttpPut]
        public async Task<IActionResult> DeleteUserAsync(String id)
        {
            var result = await _userService.DeleteUserAsync(id);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetUserByIdAsync(String id)
        {
            var result = await _userService.GetUserByIdAsync(id);
            return Ok(result);
        }


    }

}

