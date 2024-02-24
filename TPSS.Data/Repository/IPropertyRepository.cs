using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPSS.Data.Models.Entities;

namespace TPSS.Data.Repository
{
    public interface IPropertyRepository
    {
        public Task<Property> GetPropertyByIDAsync(string id);
        public Task<PropertyDetail> GetPropertyDetailByIDAsync(string id);
        public Task<Property> SearchPropertyAsync(double price1, double price2, double Area, string Province, string City);

        public Task<int> CreatePropertyAsync(Property property, PropertyDetail propertyDetail);
        public Task<int> UpdatePropertyAsync(Property property);
        public Task<int> DeletePropertyByIdAsync(string id);

        public Task<string> GetLatestPropertyIdAsync();
        public Task<string> GetLatestPropertyDetailIdAsync();
        
        public Task<string> GetOwnerIdAsync(string propertyId);
    }
}
