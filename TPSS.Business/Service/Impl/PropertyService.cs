using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPSS.Data.Models.DTO;
using TPSS.Data.Models.Entities;
using TPSS.Data.Repository;
using TPSS.Data.Repository.Impl;


namespace TPSS.Business.Service.Impl
{
    public class PropertyService : IPropertyService
    {
        private readonly IPropertyRepository _propertyRepository;

        public PropertyService(IPropertyRepository propertyRepository)
        {
            _propertyRepository = propertyRepository;
        }

        public async Task<int> CreatePropertyAsync(Property propertyDTO)
        {
            try
            {
                Property property = new Property();
                property.PropertyId = propertyDTO.PropertyId;
                property.ProjectId = propertyDTO.ProjectId;
                property.PropertyTitle = propertyDTO.PropertyTitle;
                property.Price = propertyDTO.Price;
                property.Image = propertyDTO.Image;
                property.Area = propertyDTO.Area;
                property.Province = propertyDTO.Province;
                property.City = propertyDTO.City;
                property.District = propertyDTO.District;
                property.Ward = propertyDTO.Ward;
                property.Street = propertyDTO.Street;
                property.IsDelete = propertyDTO.IsDelete;

                int result = await _propertyRepository.CreatePropertyAsync(property);
                return result;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message, e);
            }
        }
        public async Task<int> DeletePropertyAsync(string id)
        {
            try
            {
                int result = await _propertyRepository.DeletePropertyAsync(id);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task<Property> GetPropertyByIdAsync(string id)
        {
            try
            {
                Property result = await _propertyRepository.GetPropertyByIdAsync(id);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task<int> UpdatePropertyAsync(Property property)
        {
            //update soon
            throw new NotImplementedException();
        }

    }
}
