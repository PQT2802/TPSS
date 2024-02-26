using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using TPSS.Business.Common;
using TPSS.Business.Service;
using TPSS.Data.Models.DTO;
using TPSS.Data.Models.Entities;

namespace TPSS.API.Controllers
{
    [ApiController]
    [Route("api/v1.0/property")]
    [EnableCors]
    public class PropertyController : ControllerBase
    {
        public readonly IPropertyService _propertyService;
        private readonly ImageService _imageService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PropertyController(IPropertyService propertyService, IHttpContextAccessor httpContextAccessor) 
        {
            _propertyService = propertyService;
            _httpContextAccessor = httpContextAccessor;
        }
        //[HttpPost]
        //public async Task<IActionResult> CreatePropertyAsync(PropertyDTO newProperty)
        //{
        //    var result = await _propertyService.CreatePropertyAsync(newProperty);
        //    return Ok(result);
        //}

        [HttpDelete]
        public async Task<IActionResult> DeleteUserAsync(String id)
        {
            var result = await _propertyService.DeletePropertyAsync(id);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetUserByIdAsync(String id)
        {
            var result = await _propertyService.GetPropertyByIdAsync(id);
            return Ok(result);
        }
        [HttpGet("HomePage")]
        public async Task<ActionResult<IEnumerable<Property>>> GetPropertyForHomePage()
        {
            var result = await _propertyService.GetPropertyForHomePage();
            return Ok(result);
        }
        [HttpGet("Detail")]
        public async Task<ActionResult<PropertyDetailWithRelatedProperties>> GetPropertyDetailWithRelatedProperties(string propertyID)
        {
            var result = await _propertyService.GetPropertyDetailWithRelatedProperties(propertyID);
            return Ok(result);
        }
        [HttpGet("Project")]
        public async Task<ActionResult<IEnumerable<Project>>> GetAllProjects()
        {
            var result = await _propertyService.GetAllProjects();
            return Ok(result);
        }
        [HttpGet("ProjectDetail")]
        public async Task<ActionResult<ProjectDetailWithRelatedProperties>> GetProjectDetailWithRelatedProperties(string projectID)
        {
            var result = await _propertyService.GetProjectDetailWithRelatedProperties(projectID);
            return Ok(result);
        }
        [HttpGet("PropertiesByUser")]
        public async Task<ActionResult<IEnumerable<Property>>> GetProjectDetailWithRelatedProperties()
        {
            CurrentUserObject c = await TokenHepler.Instance.GetThisUserInfo(HttpContext);
            var result = await _propertyService.GetPropertiesByUserIDAsync(c.UserId);
            return Ok(result);
        }
    }
}
