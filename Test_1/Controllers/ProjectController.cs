using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TPSS.Business.Common;
using TPSS.Business.Service;
using TPSS.Business.Service.Impl;
using TPSS.Data.Models.DTO;

namespace TPSS.API.Controllers
{
    [ApiController]
    [Route("api/v1.0/project")]
    [EnableCors]
    public class ProjectController : ControllerBase
    {
        public readonly IProjectService _projectService;
        private readonly IImageService _imageService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProjectController(IProjectService projectService, IHttpContextAccessor httpContextAccessor, IImageService imageService)
        {
            _projectService = projectService;
            _httpContextAccessor = httpContextAccessor;
            _imageService = imageService;
        }

        [HttpPost("CreateProject")]
        public async Task<IActionResult> CreateProjectAsync(ProjectDTO projectDTO, string uid)
        {
            //CurrentUserObject c = await TokenHepler.Instance.GetThisUserInfo(HttpContext);
            var result = await _projectService.CreateProjectAsync(projectDTO, uid);
            return Ok(result);
        }


        [HttpGet("GetAllProjects")]
        public async Task<ActionResult<IEnumerable<dynamic>>> GetAllProjects()
        {
            var result = await _projectService.GetAllProjects();
            return Ok(result);
        }
    }
}
