using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPSS.Data.Models.DTO;
using TPSS.Data.Models.Entities;
using TPSS.Data.Repository;
using TPSS.Data.Repository.Impl;

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

        public async Task<int> DeleteProjectAsync(string projectId)
        {
            try
            {
                int result = await _projectRepository.DeleteProjectAsync(projectId);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task<Project> GetProjectByIdAsync(string projectId)
        {
            try
            {
                Project result = await _projectRepository.GetProjectByIdAsync(projectId);
                return result;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message, e);
            }
        }

        public async Task<int> UpdateProjectAsync(ProjectDTO projectDTO)
        {
            try
            {
                Project project = new Project();
                project.ProjectName = projectDTO.ProjectName;
                project.Status = projectDTO.Status;
                int result = await _projectRepository.UpdateProjectAsync(project);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
    }
}
