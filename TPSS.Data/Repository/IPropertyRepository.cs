

﻿using System;

using TPSS.Data.Models.Entities;



namespace TPSS.Data.Repository
{
    public interface IPropertyRepository
    {

        //public Task<int> CreatePropertyAsync(Property property, PropertyDetail detail) ;
        public Task<int> CreatePropertyDetailAsync(PropertyDetail detail);
        public Task<int> UpdatePropertyAsync(Property property);
        public Task<int> DeletePropertyAsync(string id);

        public Task<string> GetLatestPropertyIdAsync();
        public Task<string> GetLatestPropertyDetailIdAsync();
        public Task<string> GetProjectNameAsync(string projectID);

        public Task<IEnumerable<Property>> GetPropertyForHomePage();
        public Task<IEnumerable<Property>> GetRelatedPropertiesByCityAsync(string city);
        public Task<IEnumerable<Property>> GetRelatedPropertiesByProvinceAsync(string province);
        public Task<IEnumerable<Project>> GetAllProjects();

        public Task<UserDetail> GetOwnerByIdAsync(string ownerId);
        public Task<PropertyDetail> GetPropertyByIdAsync(string id);
        public Task<ProjectDetail> GetProjectDetail(string id);

    }
}
