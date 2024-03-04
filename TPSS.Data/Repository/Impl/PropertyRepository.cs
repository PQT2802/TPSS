
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
<<<<<<< HEAD
                var query = "INSERT INTO [Property] (PropertyID, ProjectID, PropertyTitle, Price, Image, Area, City, District, Ward, Street, IsDelete) " +
                    "VALUES(@PropertyID, @ProjectID, @PropertyTitle, @Price, @Image, @Area, @City, @District, @Ward, @Street, @IsDelete)";

                var parameter = new DynamicParameters();
                parameter.Add("PropertyID", property.PropertyId, DbType.String);
                parameter.Add("ProjectID", property.ProjectId, DbType.String);
                parameter.Add("PropertyTitle", property.PropertyTitle, DbType.String);
                parameter.Add("Price", property.Price, DbType.Double);
                parameter.Add("Image", property.Image, DbType.String);
                parameter.Add("Area", property.Area, DbType.Double);
                parameter.Add("City", property.City, DbType.String);
                parameter.Add("District", property.District, DbType.String);
                parameter.Add("Ward", property.Ward, DbType.String);
                parameter.Add("Street", property.Street, DbType.String);
                parameter.Add("IsDelete", property.IsDelete, DbType.Boolean);

=======
                
                var propertyQuery = "INSERT INTO Property (PropertyId, ProjectId, PropertyTitle, Price, Image, Area, Province, City, District, Ward, Street, IsDelete) " +
                                    "VALUES(@PropertyId, @ProjectId, @PropertyTitle, @Price, @Image, @Area, @Province, @City, @District, @Ward, @Street, @IsDelete)";
                var propertyParameters = new DynamicParameters();
                propertyParameters.Add("PropertyId", property.PropertyId, DbType.String);
                propertyParameters.Add("ProjectId", property.ProjectId, DbType.String);
                propertyParameters.Add("PropertyTitle", property.PropertyTitle, DbType.String);
                propertyParameters.Add("Price", property.Price, DbType.Double);
                //propertyParameters.Add("Image", property.Image, DbType.String);
                propertyParameters.Add("Area", property.Area, DbType.Double);
                //propertyParameters.Add("Province", property.Province, DbType.String);
                propertyParameters.Add("City", property.City, DbType.String);
                propertyParameters.Add("District", property.District, DbType.String);
                propertyParameters.Add("Ward", property.Ward, DbType.String);
                propertyParameters.Add("Street", property.Street, DbType.String);
                propertyParameters.Add("IsDelete", property.IsDelete, DbType.Boolean);
>>>>>>> DEV_THANG

                using var connection = CreateConnection();
                return await connection.ExecuteAsync(query, parameter);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<int> DeletePropertyAsync(string id)
        {
            throw new NotImplementedException();
        }

        

        public async Task<IEnumerable<HomePage>> GetPropertyForHomePage()
        {
            try
            {
                var query = "SELECT * " +
                    "FROM [dbo].[Property] AS p " +
                    "INNER JOIN [dbo].[PropertyDetail] AS pd ON p.[PropertyID] = pd.[PropertyID]";
                
<<<<<<< HEAD
                using var connection = CreateConnection();
                return await connection.QueryAsync<HomePage>(query);
=======
                var propertyDetailQuery = "INSERT INTO PropertyDetail (PropertyDetailId, PropertyId, OwnerId, PropertyTitle, Description, CreateDate, UpdateDate, CreateBy, UpdateBy, Images, Service, VerifyBy, VerifyDate) " +
                                          "VALUES(@PropertyDetailId, @PropertyId, @OwnerId, @PropertyTitle, @Description, @CreateDate, @UpdateDate, @CreateBy, @UpdateBy, @Images, @Service, @VerifyBy, @VerifyDate)";
                var propertyDetailParameters = new DynamicParameters();
                propertyDetailParameters.Add("PropertyDetailId", propertyDetail.PropertyDetailId, DbType.String);
                propertyDetailParameters.Add("PropertyId", propertyDetail.PropertyId, DbType.String);
                propertyDetailParameters.Add("OwnerId", propertyDetail.OwnerId, DbType.String);
                //propertyDetailParameters.Add("PropertyTitle", propertyDetail.PropertyTitle, DbType.String);
                propertyDetailParameters.Add("Description", propertyDetail.Description, DbType.String);
                propertyDetailParameters.Add("CreateDate", propertyDetail.CreateDate, DbType.Date);
                propertyDetailParameters.Add("UpdateDate", propertyDetail.UpdateDate, DbType.Date);
                //propertyDetailParameters.Add("CreateBy", propertyDetail.CreateBy, DbType.String);
                propertyDetailParameters.Add("UpdateBy", propertyDetail.UpdateBy, DbType.String);
                //propertyDetailParameters.Add("Images", propertyDetail.Images, DbType.String);
                propertyDetailParameters.Add("Service", propertyDetail.Service, DbType.String);
                propertyDetailParameters.Add("VerifyBy", propertyDetail.VerifyBy, DbType.String);
                propertyDetailParameters.Add("VerifyDate", propertyDetail.VerifyDate, DbType.Date);

                return await connection.ExecuteAsync(propertyDetailQuery, propertyDetailParameters);
>>>>>>> DEV_THANG
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        Task<int> IPropertyRepository.UpdatePropertyAsync(Property property)
        {
            throw new NotImplementedException();
        }

        public async Task<int> CreatePropertyDetailAsync(PropertyDetail detail)
        {
            try
            {
                var query = "INSERT INTO [PropertyDetail] (PropertyDetailID, PropertyID, OwnerID, PropertyTitle, Description, CreateDate, UpdateDate, UpdateBy, Image, Service, VerifyBy, VerifyDate, Verify, Status) " +
                    "VALUES(@PropertyDetailID, @PropertyID, @OwnerID, @PropertyTitle, @Description, @CreateDate, @UpdateDate, @UpdateBy, @Image, @Service, @VerifyBy, @VerifyDate, @Verify, @Status)";

                var parameter = new DynamicParameters();
                parameter.Add("PropertyDetailID", detail.PropertyDetailId, DbType.String);
                parameter.Add("PropertyID", detail.PropertyId, DbType.String);
                parameter.Add("OwnerID", detail.OwnerId, DbType.String);
                parameter.Add("PropertyTitle", detail.PropertyTitle, DbType.String);
                parameter.Add("Description", detail.Description, DbType.String);
                parameter.Add("CreateDate", detail.CreateDate, DbType.DateTime);
                parameter.Add("UpdateDate", detail.UpdateDate, DbType.DateTime);
                parameter.Add("UpdateBy", detail.UpdateBy, DbType.String);
                parameter.Add("Image", detail.Image, DbType.String);
                parameter.Add("Service", detail.Service, DbType.String);
                parameter.Add("VerifyBy", detail.VerifyBy, DbType.String);
                parameter.Add("VerifyDate", detail.VerifyDate, DbType.DateTime);
                parameter.Add("Verify", detail.Verify, DbType.Boolean);
                parameter.Add("Status", detail.Status, DbType.String);


                using var connection = CreateConnection();
                return await connection.ExecuteAsync(query, parameter);

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

        public async Task<PropertyDetail> GetPropertyByIdAsync(string id)
        {
            try
            {
                var query = "SELECT * " +
                    "FROM dbo.Property AS P " +
                    "JOIN dbo.PropertyDetail AS PD ON P.PropertyID = PD.PropertyID " +
                    "WHERE P.PropertyID = @PropertyID";

                var parameter = new DynamicParameters();
                parameter.Add("PropertyID", id, DbType.String);
                using var connection = CreateConnection();
                return await connection.QuerySingleAsync<PropertyDetail>(query, parameter);
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

        public async Task<IEnumerable<Property>> GetRelatedPropertiesByCityAsync(string city)
        {
            try
            {
                var query = "SELECT TOP 5 * FROM dbo.Property " +
                    "WHERE City = @City";
                var parameter = new DynamicParameters();
                parameter.Add("City", city, DbType.String);
                using var connection = CreateConnection();
                return await connection.QueryAsync<Property>(query, parameter);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task<IEnumerable<Property>> GetRelatedPropertiesByProvinceAsync(string province)
        {
            try
            {
                var query = "SELECT TOP 5 * FROM dbo.Property " +
                    "WHERE Province = @Province";
                var parameter = new DynamicParameters();
                parameter.Add("Province", province, DbType.String);
                using var connection = CreateConnection();
                return await connection.QueryAsync<Property>(query, parameter);
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
    }
}
