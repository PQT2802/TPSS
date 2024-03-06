using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        //public Task<Property> GetPropertyByIdAsync(String id);
        
        public Task<IEnumerable<dynamic>> GetPropertyForHomePage();

        public Task<dynamic> CreatePropertyAsync(PropertyDTO user, string userID);
        public Task<dynamic> DeletePropertyAsync(string propertyId);
        public Task<dynamic> UpdatePropertyAsync(PropertyDTO user);
        
        
        
        //MyProperties
        public Task<IEnumerable<dynamic>> MyProperties(string userID);

        

        //public Task<PropertyDetailWithRelatedProperties> GetPropertyDetailWithRelatedProperties(string propertyID);
        public Task<ProjectDetailWithRelatedProperties> GetProjectDetailWithRelatedProperties(string projectID);
        public Task<IEnumerable<Project>> GetAllProjects();
        public Task<IEnumerable<Project>> GetLastestProject();
        
        public Task<ProjectDetail> GetProjectDetail(string id);

        //test
        public Task<dynamic> CreatePropertyTESTAsync(PropertyDTO property, string uid);
    }
}
