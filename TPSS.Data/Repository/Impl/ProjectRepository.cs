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

        public async Task<int> CreateProjectAsync(Project project)
        {
            try
            {

                var query = "INSERT INTO [Project] (ProjectID, ProjectName, Status, IsDelete, City, District, Ward, Image) " +
                    "VALUES(@ProjectID, @ProjectName, @Status, @IsDelete, @City, @District, @Ward, @Image)";

                var parameter = new DynamicParameters();
                parameter.Add("ProjectID", project.ProjectId, DbType.String);
                parameter.Add("ProjectName", project.ProjectName, DbType.String);
                parameter.Add("Status", project.Status, DbType.String);
                parameter.Add("IsDelete", project.IsDelete, DbType.Boolean);
                parameter.Add("City", project.City, DbType.String);
                parameter.Add("District", project.District, DbType.String);
                parameter.Add("Ward", project.Ward, DbType.String);
                parameter.Add("Image", project.Image, DbType.String);

                using var connection = CreateConnection();
                return await connection.ExecuteAsync(query, parameter);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<int> CreateProjectDetailAsync(ProjectDetail detail)
        {
            try
            {

                var query = "INSERT INTO [ProjectDetail] (ProjectDetailID, ProjectID, CreateDate, UpdateDate, CreateBy, UpdateBy, ProjectDescription, Verify) " +
                    "VALUES(@ProjectDetailID, @ProjectID, @CreateDate, @UpdateDate, @CreateBy, @UpdateBy, @ProjectDescription, @Verify)";

                var parameter = new DynamicParameters();
                parameter.Add("ProjectDetailID", detail.ProjectDetailId, DbType.String);
                parameter.Add("ProjectID", detail.ProjectId, DbType.String);
                parameter.Add("CreateDate", detail.CreateDate, DbType.DateTime);
                parameter.Add("UpdateDate", detail.UpdateDate, DbType.DateTime);
                parameter.Add("CreateBy", detail.CreateBy, DbType.String);
                parameter.Add("UpdateBy", detail.UpdateBy, DbType.String);
                parameter.Add("ProjectDescription", detail.ProjectDescription, DbType.String);
                parameter.Add("Verify", detail.Verify, DbType.Boolean);

                using var connection = CreateConnection();
                return await connection.ExecuteAsync(query, parameter);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<IEnumerable<dynamic>> GetAllProjects()
        {
            try
            {
                var query = @"SELECT P.*, proj.ProjectDescription, U.Firstname + ' ' + U.Lastname AS FullName, UD.Phone, UD.Avatar " +
                    "FROM Project AS P " +
                    "INNER JOIN ProjectDetail AS proj ON p.ProjectID = proj.ProjectID " +
                    "LEFT JOIN [User] AS U ON proj.CreateBy = U.UserId " +
                    "LEFT JOIN UserDetail AS UD ON U.UserId = UD.UserID " +
                    "WHERE proj.Verify =' True'  AND P.IsDelete = 0 AND P.Status = 'Accepted';";

                using var connection = CreateConnection();
                return await connection.QueryAsync<dynamic>(query);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task<string> GetLatestProjectDetailIdAsync()
        {
            try
            {
                var query = "SELECT TOP 1 ProjectDetailID " +
                    "FROM [ProjectDetail] " +
                    "ORDER BY " +
                    "CAST(SUBSTRING(ProjectDetailID, 8, LEN(ProjectDetailID)) AS NVARCHAR) DESC, " +
                    "ProjectDetailID DESC";
                using var connection = CreateConnection();
                return await connection.QuerySingleOrDefaultAsync<string>(query);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message, e);
            }
        }

        public async Task<string> GetLatestProjectIdAsync()
        {
            try
            {
                var query = "SELECT TOP 1 ProjectID " +
                    "FROM [Project] " +
                    "ORDER BY " +
                    "CAST(SUBSTRING(ProjectID, 8, LEN(ProjectID)) AS NVARCHAR) DESC, " +
                    "ProjectID DESC";
                using var connection = CreateConnection();
                return await connection.QuerySingleOrDefaultAsync<string>(query);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message, e);
            }
        }
    }
}
