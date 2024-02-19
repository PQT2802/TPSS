

﻿using System;

using TPSS.Data.Models.Entities;



namespace TPSS.Data.Repository
{
    public interface IPropertyRepository
    {

        public Task<Property> GetPropertyByIdAsync(string id);
        public Task<IEnumerable<Property>> GetPropertyForHomePage();
        public Task<int> CreatePropertyAsync(Property property);
        public Task<int> UpdatePropertyAsync(Property property);
        public Task<int> DeletePropertyAsync(string id);

    }
}
