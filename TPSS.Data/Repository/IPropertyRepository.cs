

﻿using System;

using TPSS.Data.Models.Entities;



namespace TPSS.Data.Repository
{
    public interface IPropertyRepository
    {

        public Task<PropertyDetail> GetPropertyByIdAsync(string id);
        public Task<IEnumerable<Property>> GetPropertyForHomePage();
        //public Task<int> CreatePropertyAsync(Property property, PropertyDetail detail) ;
        public Task<int> CreatePropertyDetailAsync(PropertyDetail detail);
        public Task<int> UpdatePropertyAsync(Property property);
        public Task<int> DeletePropertyAsync(string id);
        public Task<string> GetLatestPropertyIdAsync();
        public Task<string> GetLatestPropertyDetailIdAsync();
        public Task<IEnumerable<Property>> GetRelatedPropertiesAsync(string city);
        public Task<IEnumerable<Project>> GetAllProjects();
    }
}
