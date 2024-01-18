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
        
        public async Task<IActionResult> CreateUser (UserDTO newUser)
        {
            var result = await _userService.CreateUserAsync(newUser);
            return Ok(result);//tra ve respone(status:200,body:result)
        }
    }
        
    }

