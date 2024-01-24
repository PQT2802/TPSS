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

        public async Task<int> DeleteProjectAsync(string projectId)
        {
            try
            {
                var query = "UPDATE Project " +
                    "SET IsDelete = true" +
                    "WHERE ProjectId = @ProjectId";
                var parameter = new DynamicParameters();
                parameter.Add("ProjectId", projectId, DbType.String);
                using var connection = CreateConnection();
                return await connection.ExecuteAsync(query, parameter);

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
                var query = "SELECT *" +
                    "FROM Project" +
                    "WHERE ProjectId = @ProjectId";
                var parameter = new DynamicParameters();
                parameter.Add("ProjectId", projectId, DbType.String);
                using var connection = CreateConnection();
                return await connection.QuerySingleAsync<Project>(query, parameter);

            }
            catch (Exception e)
            {

                throw new Exception(e.Message, e);
            }
        }

        public async Task<int> UpdateProjectAsync(Project updateProject)
        {
            try
            {
                var query = "UPDATE Project" +
                 "SET ProjectId = @ProjectId, ProjectName = @ProjectName, Status = @Status" +
                 "WHERE ProjectId = @ProjectId";

                var parameter = new DynamicParameters();
                parameter.Add("Email", updateProject.ProjectId, DbType.String);
                parameter.Add("Password", updateProject.ProjectName, DbType.String);
                parameter.Add("Username", updateProject.Status, DbType.String);
                using var connection = CreateConnection();
                return await connection.ExecuteAsync(query, parameter);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message, e);
            }
        }
    }
}
