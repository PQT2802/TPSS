<<<<<<< HEAD

﻿using System;

using TPSS.Data.Models.Entities;


=======
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPSS.Data.Models.Entities;

>>>>>>> DEV_THANG
namespace TPSS.Data.Repository
{
    public interface IPropertyRepository
    {
<<<<<<< HEAD

        public Task<Property> GetPropertyByIdAsync(string id);
        public Task<IEnumerable<Property>> GetPropertyForHomePage();
        public Task<int> CreatePropertyAsync(Property property);
        public Task<int> UpdatePropertyAsync(Property property);
        public Task<int> DeletePropertyAsync(string id);

=======
        public Task<Property> GetPropertyByIDAsync(string id);
        public Task<PropertyDetail> GetPropertyDetailByIDAsync(string id);
        public Task<Property> SearchPropertyAsync(double price1, double price2, double Area, string Province, string City);

        public Task<int> CreatePropertyAsync(Property property, PropertyDetail propertyDetail);
        public Task<int> UpdatePropertyAsync(Property property);
        public Task<int> DeletePropertyByIdAsync(string id);

        public Task<string> GetLatestPropertyIdAsync();
        public Task<string> GetLatestPropertyDetailIdAsync();
        
>>>>>>> DEV_THANG
    }
}
