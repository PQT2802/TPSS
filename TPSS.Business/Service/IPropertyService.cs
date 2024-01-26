using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPSS.Data.Models.DTO;
using TPSS.Data.Models.Entities;

namespace TPSS.Business.Service
{
    public interface IPropertyService
    {
        public Task<Property> GetPropertyByIdAsync(string id);
        public Task<int> CreatePropertyAsync(Property property);
        public Task<int> UpdatePropertyAsync(Property property);
        public Task<int> DeletePropertyAsync(string id);
    }
}
