using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TPSS.Business.Service;
using TPSS.Business.Service.Impl;
using TPSS.Data.Models.DTO;

namespace TPSS.API.Controllers
{
    [ApiController]
    [Route("api/v1.0/users")]
    [EnableCors]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProjectAsync(ProjectDTO newProject)
        {
            var result = await _projectService.CreateProjectAsync(newProject);
            return Ok(result);//tra ve respone(status:200,body:result)
        }

        [HttpPut]
        public async Task<IActionResult> DeleteProjectAsync(String id)
        {
            var result = await _projectService.DeleteProjectAsync(id);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetProjectByIdAsync(String id)
        {
            var result = await _projectService.GetProjectByIdAsync(id);
            return Ok(result);
        }
    }
}
