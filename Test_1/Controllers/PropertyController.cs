﻿using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using TPSS.Business.Service;
using TPSS.Data.Models.DTO;

namespace TPSS.API.Controllers
{
    [ApiController]
    [Route("api/v1.0/property")]
    [EnableCors]
    public class PropertyController : ControllerBase
    {
        public readonly IPropertyService _propertyService;
        public PropertyController(IPropertyService propertyService) 
        {
            _propertyService = propertyService;
        }
        [HttpPost]
        public async Task<IActionResult> CreatePropertyAsync(PropertyDTO newProperty)
        {
            var result = await _propertyService.CreatePropertyAsync(newProperty);
            return Ok(result);
        }

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

    }
}