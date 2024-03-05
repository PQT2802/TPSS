using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using TPSS.Business.Common;
using TPSS.Business.Service;
using TPSS.Business.Service.Impl;
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
        private readonly IImageService _imageService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PropertyController(IPropertyService propertyService, IHttpContextAccessor httpContextAccessor, IImageService imageService) 
        {
            _propertyService = propertyService;
            _httpContextAccessor = httpContextAccessor;
            _imageService = imageService;
        }

        [HttpGet("HomePage")]
        public async Task<ActionResult<IEnumerable<dynamic>>> GetPropertyForHomePage()
        {
            var result = await _propertyService.GetPropertyForHomePage();
            return Ok(result);
        }

        //[HttpGet("PropertyDetail")]
        //public async Task<ActionResult<PropertyDetailWithRelatedProperties>> GetPropertyDetailWithRelatedProperties(string propertyID)
        //{
        //    var result = await _propertyService.GetPropertyDetailWithRelatedProperties(propertyID);
        //    return Ok(result);
        //}

        [HttpGet("PropertiesByUser")]
        public async Task<ActionResult<IEnumerable<Property>>> GetProjectDetailWithRelatedProperties()
        {
            CurrentUserObject c = await TokenHepler.Instance.GetThisUserInfo(HttpContext);
            var result = await _propertyService.GetPropertiesByUserIDAsync(c.UserId);
            return Ok(result);
        }

        [HttpPost("CreateProperty")]
        public async Task<IActionResult> CreatePropertyAsync(PropertyDTO propertyDTO)
        {
            CurrentUserObject c = await TokenHepler.Instance.GetThisUserInfo(HttpContext);
            var result = await _propertyService.CreatePropertyAsync(propertyDTO,c.UserId);
            return Ok(result);
        }

        // test add with no cockies
        [HttpPost("CreatePropertyTEST")]
        public async Task<IActionResult> CreatePropertyTESTAsync(PropertyDTO propertyDTO)
        {
            var result = await _propertyService.CreatePropertyTESTAsync(propertyDTO);
            return Ok(result);
        }






        //Project

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

        [HttpGet("LastestProject")]
        public async Task<ActionResult<IEnumerable<Project>>> GetLastestProject()
        {
            var result = await _propertyService.GetLastestProject();
            return Ok(result);
        }


        ///test image
        [HttpPost("Image")]
        public async Task<IActionResult> LinkFolderCheck(IFormFileCollection image, string folderName)
        {

            var result = await _imageService.LinkFolderCheck(image, folderName);

            return Ok(result);
        }

        //[HttpPost("Images")]
        //public async Task<IActionResult> UploadMultipleImagesToFirebaseStorage(IFormFileCollection thumbnails, string folderName)
        //{

        //    var result = await _imageService.UploadMultipleImagesToFirebaseStorage(thumbnails, folderName);

        //    return Ok(result);
        //}

        //[HttpPost("ImagesProper")]
        //public async Task<IActionResult> UploadImagesForPropertyDetail(IFormFileCollection thumbnails, string folderName)
        //{

        //    var result = await _imageService.UploadImagesForProperty(thumbnails, folderName);

        //    return Ok(result);
        //}


    }
}
