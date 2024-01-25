using System;
using System.Collections.Generic;
using System.Linq;
using TPSS.Data.Models.DTO;
using TPSS.Data.Models.Entities;
using TPSS.Data.Repository;
namespace TPSS.Business.Service.Impl
{
    internal class PropertyService : IPropertyService
    {
        public readonly IPropertyRepository _propertyRepository;
        public PropertyService(IPropertyRepository propertyRepository)
        {
            _propertyRepository = propertyRepository;
        }

        public Task<int> CreatePropertyAsync(PropertyDTO user)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeletePropertyAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Property> GetPropertyByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdatePropertyAsync(PropertyDTO user)
        {
            throw new NotImplementedException();
        }
    }
}