using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPSS.Data.Helper;
using TPSS.Data.Models.Entities;

namespace TPSS.Data.Repository.Impl
{
    public class ProjectRepository : BaseRepository, IProjectRepository
    {
        public ProjectRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<int> CreateProjectAsync(Project createProject)
        {
            try
            {
                var query = "INSERT INTO Project (ProjectId, ProjectName, Status)" +
                    "VALUE (@ProjectId, @ProjectName, @Status)";
                var parameter = new DynamicParameters();
                parameter.Add("ProjectId", createProject.ProjectId, DbType.String);
                parameter.Add("ProjectName", createProject.ProjectName, DbType.String);
                parameter.Add("Status", createProject.Status, DbType.String);
                using var connection = CreateConnection();
                return await connection.ExecuteAsync(query, parameter);

            }
            catch (Exception e)
            {

                throw new Exception(e.Message, e);
            }
        }

        Task<int> IProjectRepository.DeleteProjectAsync(string projectId)
        {
            throw new NotImplementedException();
        }

        Task<Project> IProjectRepository.GetProjectByIdAsync(string projectId)
        {
            throw new NotImplementedException();
        }

        Task<int> IProjectRepository.UpdateProjectAsync(Project updateProject)
        {
            throw new NotImplementedException();
        }
    }
}
