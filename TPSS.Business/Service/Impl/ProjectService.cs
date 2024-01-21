using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPSS.Data.Models.DTO;
using TPSS.Data.Models.Entities;
using TPSS.Data.Repository;

namespace TPSS.Business.Service.Impl
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<int> CreateProjectAsync(ProjectDTO projectDTO)
        {
            try
            {
                Project project = new Project();
                project.ProjectId = projectDTO.ProjectId;
                project.ProjectName = projectDTO.ProjectName;
                project.Status = projectDTO.Status;
                int result = await _projectRepository.CreateProjectAsync(project);
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        Task<int> IProjectService.DeleteProjectAsync(string projectId)
        {
            throw new NotImplementedException();
        }

        Task<Project> IProjectService.GetProjectByIdAsync(string projectId)
        {
            throw new NotImplementedException();
        }

        Task<int> IProjectService.UpdateProjectAsync(ProjectDTO updateProject)
        {
            throw new NotImplementedException();
        }
    }
}
