
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration _configuration;
        public PropertyService(IPropertyRepository propertyRepository, IConfiguration configuration)
        {
            _propertyRepository = propertyRepository;
        }

        


        public async Task<int> CreatePropertyAsync(PropertyDTO propertyDTO)
        {
            throw new NotImplementedException();
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

        public async Task<int> UpdatePropertyAsync(PropertyDTO property)
        {
            //update soon
            throw new NotImplementedException();
        }    

        public async Task<IEnumerable<Property>> GetPropertyForHomePage()
        {
            try
            {

                IEnumerable<Property> result = await _propertyRepository.GetPropertyForHomePage();
                return result;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message, e);
            }
        }
    }
}
