using Microsoft.AspNetCore.Http;
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
        public Task<Property> GetPropertyByIdAsync(String id);
        public Task<IEnumerable<HomePage>> GetPropertyForHomePage();
        public Task<dynamic> CreatePropertyAsync(PropertyDTO user, string userID);
        public Task<dynamic> UpdatePropertyAsync(PropertyDTO user);
        public Task<int> DeletePropertyAsync(String id);
        public Task<PropertyDetailWithRelatedProperties> GetPropertyDetailWithRelatedProperties(string propertyID);
        public Task<ProjectDetailWithRelatedProperties> GetProjectDetailWithRelatedProperties(string projectID);
        public Task<IEnumerable<Project>> GetAllProjects();
        public Task<IEnumerable<Project>> GetLastestProject();
        public Task<IEnumerable<Property>> GetPropertiesByUserIDAsync(string UserID);
        public Task<ProjectDetail> GetProjectDetail(string id);

    }
}
