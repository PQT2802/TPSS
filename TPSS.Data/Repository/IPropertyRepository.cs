

﻿using System;

using TPSS.Data.Models.Entities;



namespace TPSS.Data.Repository
{
    public interface IPropertyRepository
    {

        public Task<int> CreatePropertyAsync(Property property) ;
        public Task<int> CreatePropertyDetailAsync(PropertyDetail detail);
        public Task<int> UpdatePropertyAsync(Property property);
        public Task<int> UpdatePropertyDetailAsync(PropertyDetail property);
        public Task<dynamic> DeletePropertyAsync(string id);

        public Task<string> GetLatestPropertyIdAsync();
        public Task<string> GetLatestPropertyDetailIdAsync();

        public Task<string> GetProjectNameAsync(string projectID);

        public Task<IEnumerable<dynamic>> GetPropertyForHomePage();

        public Task<IEnumerable<dynamic>> MyProperties(string userID);
        public Task<IEnumerable<dynamic>> GetVerifyPropertiesAsync();
        public Task<IEnumerable<dynamic>> GetWaitingPropertiesAsync();
        public Task<dynamic> VerifyPropertiesAsync(List<string> propertiesID);
        public Task<dynamic> AcceptedPropertiesAsync(List<string> propertiesID);


        public Task<dynamic> GetPropertyByIdAsync(string id);
        public Task<IEnumerable<dynamic>> GetRelatedPropertiesByCityAsync(string city);
        public Task<IEnumerable<dynamic>> GetRelatedPropertiesByDistrictAsync(string District);


        public Task<IEnumerable<Property>> GetPropertiesByUserIDAsync(string UserID);
        
        public Task<IEnumerable<Property>> GetRelatedPropertiesByProjectIDAsync(string projectID);
        public Task<IEnumerable<Project>> GetAllProjects();
        public Task<IEnumerable<Project>> GetLastestProject();

        public Task<UserDetail> GetOwnerByIdAsync(string ownerId);
        
        public Task<ProjectDetail> GetProjectDetail(string id);

        //public Task<int> CreateAlbumAsync(string propertyId, List<string> images);



        public Task<string> GetOwnerIdAsync(string propertyId);

    }
}
