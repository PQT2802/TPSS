using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPSS.Data.Models.DTO;

namespace TPSS.Business.Service
{
    public interface IProjectService
    {
        public Task<IEnumerable<dynamic>> GetAllProjects();
        public Task<dynamic> CreateProjectAsync(ProjectDTO projectDTO, string userID);
        public Task<dynamic> ProjectDetailByIdAsync(string projectId);
    }
}
