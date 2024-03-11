
﻿using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using TPSS.Data.Helper;
using TPSS.Data.Models.Entities;



using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPSS.Data.Helper;
using TPSS.Data.Models.Entities;
using System.Reflection.Metadata;
using System.Data.Common;
using System.Collections;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;

namespace TPSS.Data.Repository.Impl
{
    public class PropertyRepository : BaseRepository, IPropertyRepository
    {
        public PropertyRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<int> CreatePropertyAsync(Property property)
        {
            try
            {

                var query = "INSERT INTO [Property] (PropertyID, ProjectID, PropertyTitle, Price, Area, City, District, Ward, Street, IsDelete) " +
                    "VALUES(@PropertyID, @ProjectID, @PropertyTitle, @Price, @Area, @City, @District, @Ward, @Street, @IsDelete)";

                var parameter = new DynamicParameters();
                parameter.Add("PropertyID", property.PropertyId, DbType.String);
                parameter.Add("ProjectID", property.ProjectId, DbType.String);
                parameter.Add("PropertyTitle", property.PropertyTitle, DbType.String);
                parameter.Add("Price", property.Price, DbType.Double);
                parameter.Add("Area", property.Area, DbType.Double);
                parameter.Add("City", property.City, DbType.String);
                parameter.Add("District", property.District, DbType.String);
                parameter.Add("Ward", property.Ward, DbType.String);
                parameter.Add("Street", property.Street, DbType.String);
                parameter.Add("IsDelete", property.IsDelete, DbType.Boolean);

                using var connection = CreateConnection();
                return await connection.ExecuteAsync(query, parameter);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<int> CreatePropertyDetailAsync(PropertyDetail detail)
        {
            try
            {
                var query = "INSERT INTO [PropertyDetail] (PropertyDetailID, PropertyID, OwnerID, Description, CreateDate, UpdateDate, UpdateBy, Service, VerifyBy, VerifyDate, Verify, Status, CreateBy) " +
                    "VALUES(@PropertyDetailID, @PropertyID, @OwnerID, @Description, @CreateDate, @UpdateDate, @UpdateBy, @Service, @VerifyBy, @VerifyDate, @Verify, @Status, @CreateBy)";

                var parameter = new DynamicParameters();
                parameter.Add("PropertyDetailID", detail.PropertyDetailId, DbType.String);
                parameter.Add("PropertyID", detail.PropertyId, DbType.String);
                parameter.Add("OwnerID", detail.OwnerId, DbType.String);
                parameter.Add("Description", detail.Description, DbType.String);
                parameter.Add("CreateDate", detail.CreateDate, DbType.DateTime);
                parameter.Add("UpdateDate", detail.UpdateDate, DbType.DateTime);
                parameter.Add("UpdateBy", detail.UpdateBy, DbType.String);
                parameter.Add("Service", detail.Service, DbType.String);
                parameter.Add("VerifyBy", detail.VerifyBy, DbType.String);
                parameter.Add("VerifyDate", detail.VerifyDate, DbType.DateTime);
                parameter.Add("Verify", detail.Verify, DbType.Boolean);
                parameter.Add("Status", detail.Status, DbType.String);
                parameter.Add("CreateBy", detail.CreateBy, DbType.String);


                using var connection = CreateConnection();
                return await connection.ExecuteAsync(query, parameter);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<dynamic> DeletePropertyAsync(string id)
        {
            try
            {
                var query = @"UPDATE [dbo].[Property] SET IsDelete = 'True' WHERE PropertyID = @PropertyID;";

                var parameter = new DynamicParameters();
                parameter.Add("PropertyID", id, DbType.String);
                using var connection = CreateConnection();
                return await connection.ExecuteAsync(query, parameter);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<IEnumerable<dynamic>> GetPropertyForHomePage()
        {
            try
            {
                var query = "SELECT P.*, proj.ProjectName, U.Firstname + ' ' + U.Lastname AS FullName, UD.Phone, UD.Avatar, PD.Description, A.Image " +
                    "FROM Property AS P " +
                    "INNER JOIN Project AS proj ON p.ProjectID = proj.ProjectID " +
                    "INNER JOIN PropertyDetail AS PD ON P.PropertyID = PD.PropertyID " +
                    "LEFT JOIN [User] AS U ON PD.OwnerID = U.UserId " +
                    "LEFT JOIN UserDetail AS UD ON U.UserId = UD.UserID " +
                    "LEFT JOIN Album AS A ON P.PropertyID = A.PropertyId " +
                    "WHERE A.ImageDescription = 'HomePage' AND P.IsDelete = 0 AND PD.Status = 'Accepted';";   

                using var connection = CreateConnection();
                return await connection.QueryAsync<dynamic>(query);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task<int> UpdatePropertyAsync(Property property)
        {
            try
            {
                var query = "UPDATE [Property] SET " +
                            "ProjectID = @ProjectID, " +
                            "PropertyTitle = @PropertyTitle, " +
                            "Price = @Price, " +
                            "Area = @Area, " +
                            "City = @City, " +
                            "District = @District, " +
                            "Ward = @Ward, " +
                            "Street = @Street, " +
                            "IsDelete = @IsDelete " +
                            "WHERE PropertyID = @PropertyID";

                var parameters = new DynamicParameters();
                
                parameters.Add("ProjectID", property.ProjectId, DbType.String);
                parameters.Add("PropertyTitle", property.PropertyTitle, DbType.String);
                parameters.Add("Price", property.Price, DbType.Double);
                parameters.Add("Area", property.Area, DbType.Double);
                parameters.Add("City", property.City, DbType.String);
                parameters.Add("District", property.District, DbType.String);
                parameters.Add("Ward", property.Ward, DbType.String);
                parameters.Add("Street", property.Street, DbType.String);
                parameters.Add("IsDelete", property.IsDelete, DbType.Boolean);
                parameters.Add("PropertyID", property.PropertyId, DbType.String);

                using var connection = CreateConnection();
                return await connection.ExecuteAsync(query, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }



        public async Task<string> GetLatestPropertyIdAsync()
        {
            try
            {
                var query = "SELECT TOP 1 PropertyID " +
                    "FROM [Property] " +
                    "ORDER BY " +
                    "CAST(SUBSTRING(PropertyID, 8, LEN(PropertyID)) AS NVARCHAR) DESC, " +
                    "PropertyID DESC";
                using var connection = CreateConnection();
                return await connection.QuerySingleOrDefaultAsync<string>(query);   
            }
            catch (Exception e)
            {

                throw new Exception(e.Message, e);
            }
        }

        public async Task<string> GetLatestPropertyDetailIdAsync()
        {
            try
            {
                var query = "SELECT TOP 1 PropertyDetailID " +
                    "FROM [PropertyDetail] " +
                    "ORDER BY " +
                    "CAST(SUBSTRING(PropertyDetailID, 8, LEN(PropertyDetailID)) AS NVARCHAR) DESC, " +
                    "PropertyDetailID DESC";
                using var connection = CreateConnection();
                return await connection.QuerySingleOrDefaultAsync<string>(query);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message, e);
            }
        }

        public async Task<dynamic> GetPropertyByIdAsync(string id)
        {
            try
            {
                var query = "SELECT P.*, proj.ProjectName, U.Firstname + ' ' + U.Lastname AS FullName, UD.Phone, UD.Avatar, PD.Description, PD.Verify, PD.Status, PD.Service " +
                    "FROM Property AS P " +
                    "INNER JOIN Project AS proj ON p.ProjectID = proj.ProjectID " +
                    "INNER JOIN PropertyDetail AS PD ON P.PropertyID = PD.PropertyID " +
                    "LEFT JOIN [User] AS U ON PD.OwnerID = U.UserId " +
                    "LEFT JOIN UserDetail AS UD ON U.UserId = UD.UserID " +
                    "WHERE P.PropertyID = @PropertyID;";

                var parameter = new DynamicParameters();
                parameter.Add("PropertyID", id, DbType.String);
                using var connection = CreateConnection();
                return await connection.QuerySingleAsync<dynamic>(query, parameter);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task<string> GetLatestImageIdAsync()
        {
            try
            {
                var query = "SELECT TOP 1 ImageId " +
                    "FROM [Album] " +
                    "ORDER BY " +
                    "CAST(SUBSTRING(ImageId, 8, LEN(ImageId)) AS NVARCHAR) DESC, " +
                    "ImageId DESC";
                using var connection = CreateConnection();
                return await connection.QuerySingleOrDefaultAsync<string>(query);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message, e);
            }
        }


        public async Task<IEnumerable<Project>> GetAllProjects()
        {
            try
            {
                var query = "SELECT * FROM dbo.Project";

                using var connection = CreateConnection();
                return await connection.QueryAsync<Project>(query);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task<IEnumerable<dynamic>> GetRelatedPropertiesByCityAsync(string city)
        {
            try
            {
                var query = "SELECT TOP 10 P.*, proj.ProjectName, U.Firstname + ' ' + U.Lastname AS FullName, UD.Phone, UD.Avatar, PD.Description, A.Image " +
                    "FROM Property AS P " +
                    "INNER JOIN Project AS proj ON p.ProjectID = proj.ProjectID " +
                    "INNER JOIN PropertyDetail AS PD ON P.PropertyID = PD.PropertyID " +
                    "LEFT JOIN [User] AS U ON PD.OwnerID = U.UserId " +
                    "LEFT JOIN UserDetail AS UD ON U.UserId = UD.UserID " +
                    "LEFT JOIN Album AS A ON P.PropertyID = A.PropertyId " +
                    "WHERE A.ImageDescription = 'HomePage' AND P.IsDelete = 0 AND P.City =@City AND PD.Status = 'Accepted';";
                var parameter = new DynamicParameters();
                parameter.Add("City", city, DbType.String);
                using var connection = CreateConnection();
                return await connection.QueryAsync<dynamic>(query, parameter);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task<IEnumerable<dynamic>> GetRelatedPropertiesByDistrictAsync(string District)
        {
            try
            {
                var query = "SELECT TOP 10 P.*, proj.ProjectName, U.Firstname + ' ' + U.Lastname AS FullName, UD.Phone, UD.Avatar, PD.Description, A.Image " +
                    "FROM Property AS P " +
                    "INNER JOIN Project AS proj ON p.ProjectID = proj.ProjectID " +
                    "INNER JOIN PropertyDetail AS PD ON P.PropertyID = PD.PropertyID " +
                    "LEFT JOIN [User] AS U ON PD.OwnerID = U.UserId " +
                    "LEFT JOIN UserDetail AS UD ON U.UserId = UD.UserID " +
                    "LEFT JOIN Album AS A ON P.PropertyID = A.PropertyId " +
                    "WHERE A.ImageDescription = 'HomePage' AND P.IsDelete = 0 AND P.District =@District AND PD.Status = 'Accepted';";
                var parameter = new DynamicParameters();
                parameter.Add("District", District, DbType.String);
                using var connection = CreateConnection();
                return await connection.QueryAsync<dynamic>(query, parameter);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task<UserDetail> GetOwnerByIdAsync(string ownerId)
        {
            try
            {
                var query = "SELECT u.*, ud.* " +
                    "FROM [dbo].[User] u " +
                    "INNER JOIN UserDetail ud ON u.UserId = ud.UserID " +
                    "WHERE u.UserId = @value";
                var parameter = new DynamicParameters();
                parameter.Add("value", ownerId, DbType.String);
                using var connection = CreateConnection();
                return await connection.QueryFirstOrDefaultAsync<UserDetail>(query, parameter);

            }
            catch (Exception e)
            {

                throw new Exception(e.Message, e);
            }
        }

        public async Task<string> GetProjectNameAsync(string projectID)
        {
            try
            {
                var query = "SELECT ProjectName " +
                    "FROM dbo.Project " +
                    "WHERE ProjectID = @value";
                var parameter = new DynamicParameters();
                parameter.Add("value", projectID, DbType.String);
                using var connection = CreateConnection();
                return await connection.QueryFirstOrDefaultAsync<string>(query, parameter);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task<ProjectDetail> GetProjectDetail(string id)
        {
            try
            {
                var query = "SELECT * " +
                    "FROM dbo.ProjectDetail " +
                    "WHERE ProjectID = @value";
                var parameter = new DynamicParameters();
                parameter.Add("value", id, DbType.String);
                using var connection = CreateConnection();
                return await connection.QueryFirstOrDefaultAsync<ProjectDetail>(query, parameter);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task<IEnumerable<Property>> GetRelatedPropertiesByProjectIDAsync(string projectID)
        {
            try
            {
                var query = "SELECT * FROM dbo.Property " +
                    "WHERE ProjectID = @ProjectID";
                var parameter = new DynamicParameters();
                parameter.Add("ProjectID", projectID, DbType.String);
                using var connection = CreateConnection();
                return await connection.QueryAsync<Property>(query, parameter);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task<IEnumerable<Property>> GetPropertiesByUserIDAsync(string UserID)
        {
            try
            {
                var query = "SELECT * " +
                    "FROM [dbo].[Property] AS P " +
                    "JOIN [dbo].[PropertyDetail] AS PD ON P.[PropertyID] = PD.[PropertyID] " +
                    "WHERE PD.[OwnerID] = @UserID";
                var parameter = new DynamicParameters();
                parameter.Add("UserID", UserID, DbType.String);
                using var connection = CreateConnection();
                return await connection.QueryAsync<Property>(query, parameter);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        public async Task<string> GetOwnerIdAsync(string propertyId)
        {
            try
            {
                var query = "SELECT OwnerId FROM PropertyDetail WHERE PropertyId = @PropertyId";
                var parameters = new { PropertyId = propertyId };
                using var connection = CreateConnection();
                return await connection.QuerySingleOrDefaultAsync<string>(query, parameters);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message, e);
            }
        }

        public async Task<IEnumerable<Project>> GetLastestProject()
        {
            try
            {
                var query = "SELECT TOP 10 * FROM dbo.Project";

                using var connection = CreateConnection();
                return await connection.QueryAsync<Project>(query);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }


        public async Task<IEnumerable<dynamic>> MyProperties(string userID)
        {
            try
            {
                var query = "SELECT P.*, PD.*, A.Image AS HomePageImage " +
                    "FROM Property AS P " +
                    "INNER JOIN Project AS proj ON p.ProjectID = proj.ProjectID " +
                    "INNER JOIN PropertyDetail AS PD ON P.PropertyID = PD.PropertyID " +
                    "LEFT JOIN Album AS A ON P.PropertyID = A.PropertyId " +
                    "WHERE A.ImageDescription = 'HomePage' AND PD.OwnerID =@OwnerID;";

                var parameter = new DynamicParameters();
                parameter.Add("OwnerID", userID, DbType.String);
                using var connection = CreateConnection();
                return await connection.QueryAsync<Property>(query, parameter);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task<IEnumerable<dynamic>> GetVerifyPropertiesAsync()
        {
            try
            {
                var query = "SELECT P.*, PD.*, A.Image AS HomePageImage " +
                    "FROM Property AS P " +
                    "INNER JOIN Project AS proj ON p.ProjectID = proj.ProjectID " +
                    "INNER JOIN PropertyDetail AS PD ON P.PropertyID = PD.PropertyID " +
                    "LEFT JOIN Album AS A ON P.PropertyID = A.PropertyId " +
                    "WHERE A.ImageDescription = 'HomePage' AND PD.Verify = 'False';";

                using var connection = CreateConnection();
                return await connection.QueryAsync<dynamic>(query);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task<IEnumerable<dynamic>> GetWaitingPropertiesAsync()
        {
            try
            {
                var query = "SELECT P.*, PD.*, A.Image AS HomePageImage " +
                    "FROM Property AS P " +
                    "INNER JOIN Project AS proj ON p.ProjectID = proj.ProjectID " +
                    "INNER JOIN PropertyDetail AS PD ON P.PropertyID = PD.PropertyID " +
                    "LEFT JOIN Album AS A ON P.PropertyID = A.PropertyId " +
                    "WHERE A.ImageDescription = 'HomePage' AND PD.Status = 'Waiting';";

                using var connection = CreateConnection();
                return await connection.QueryAsync<dynamic>(query);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task<dynamic> VerifyPropertiesAsync(List<string> propertiesID)
        {
            try
            {
                using var connection = CreateConnection() as SqlConnection;
                await connection.OpenAsync();

                foreach (string propertyID in propertiesID)
                {
                    
                    string updateQuery = "UPDATE [dbo].[PropertyDetail] SET " +
                        "Verify = 1 " +
                        "WHERE PropertyID = @PropertyID;";

                    
                    await connection.ExecuteAsync(updateQuery, new { PropertyID = propertyID });
                }

                return propertiesID.Count;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task<dynamic> AcceptedPropertiesAsync(List<string> propertiesID)
        {
            try
            {
                
                using var connection = CreateConnection() as SqlConnection;
                await connection.OpenAsync();

                foreach (string propertyID in propertiesID)
                {
                    
                    string updateQuery = "UPDATE [dbo].[PropertyDetail] SET " +
                        "Status = 'Accepted' " +
                        "WHERE PropertyID = @PropertyID;";

                    
                    await connection.ExecuteAsync(updateQuery, new { PropertyID = propertyID });
                }

                return propertiesID.Count;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }




        public async Task<int> UpdatePropertyDetailAsync(PropertyDetail detail)
        {
            try
            {
                var query = "UPDATE [PropertyDetail] SET " +
                            "Description = @Description, " +
                            "UpdateDate = @UpdateDate, " +
                            "UpdateBy = @UpdateBy " +
                            "WHERE PropertyDetailID = @PropertyDetailID";

                var parameters = new DynamicParameters();
                parameters.Add("Description", detail.Description, DbType.String);
                parameters.Add("UpdateDate", detail.UpdateDate, DbType.DateTime);
                parameters.Add("UpdateBy", detail.UpdateBy, DbType.String);
                parameters.Add("PropertyDetailID", detail.PropertyDetailId, DbType.String);

                using var connection = CreateConnection();
                return await connection.ExecuteAsync(query, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        
    }
}
