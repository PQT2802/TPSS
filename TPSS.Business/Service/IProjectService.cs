using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPSS.Data.Models.DTO;
using TPSS.Data.Models.Entities;

namespace TPSS.Business.Service
{
    public interface IProjectService
    {
        //C
        public Task<int> CreateProjectAsync(ProjectDTO projectDTO);
        //U
        public Task<int> UpdateProjectAsync(ProjectDTO projectDTO);
        //D
        public Task<int> DeleteProjectAsync(string projectId);
        //R
        public Task<Project> GetProjectByIdAsync(string projectId);
    }
}

