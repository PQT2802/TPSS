using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPSS.Data.Models.Entities;

namespace TPSS.Data.Repository
{
    public interface IProjectRepository
    {
        //C
        public Task<int> CreateProjectAsync(Project createProject);
        //U
        public Task<int> UpdateProjectAsync(Project updateProject);
        //D
        public Task<int> DeleteProjectAsync(string projectId);
        //R
        public Task<Project> GetProjectByIdAsync(string projectId);
    }
}
