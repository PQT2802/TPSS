
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

        //public async Task<int> CreatePropertyAsync(Property property, PropertyDetail propertyDetail)
        //{
        //    try
        //    {
        //        var query = "INSERT INTO [Property] (PropertyID, ProjectID, PropertyTitle, Price, Image, Area, Province, City, District, Ward, Street, IsDelete) " +
        //            "VALUES(@PropertyID, @ProjectID, @PropertyTitle, @Price, @Image, @Area, @Province, @City, @District, @Ward, @Street, @IsDelete)";
                
        //        var parameter = new DynamicParameters();
        //        parameter.Add("PropertyID", property.PropertyId, DbType.String);
        //        parameter.Add("ProjectID", property.ProjectId, DbType.String);
        //        parameter.Add("PropertyTitle", property.PropertyTitle, DbType.String);
        //        parameter.Add("Price", property.Price, DbType.Double);


        //        using var connection = CreateConnection();
        //        return await connection.ExecuteAsync(query, parameter);


                
        //        var propertyDetailQuery = "INSERT INTO PropertyDetail (PropertyDetailId, PropertyId, OwnerId, PropertyTitle, Description, CreateDate, UpdateDate, CreateBy, UpdateBy, Images, Service, VerifyBy, VerifyDate) " +
        //                                  "VALUES(@PropertyDetailId, @PropertyId, @OwnerId, @PropertyTitle, @Description, @CreateDate, @UpdateDate, @CreateBy, @UpdateBy, @Images, @Service, @VerifyBy, @VerifyDate)";
        //        var propertyDetailParameters = new DynamicParameters();
        //        propertyDetailParameters.Add("PropertyDetailId", propertyDetail.PropertyDetailId, DbType.String);
        //        propertyDetailParameters.Add("PropertyId", propertyDetail.PropertyId, DbType.String);
        //        propertyDetailParameters.Add("OwnerId", propertyDetail.OwnerId, DbType.String);
        //        propertyDetailParameters.Add("PropertyTitle", propertyDetail.PropertyTitle, DbType.String);
        //        propertyDetailParameters.Add("Description", propertyDetail.Description, DbType.String);
        //        propertyDetailParameters.Add("CreateDate", propertyDetail.CreateDate, DbType.Date);
        //        propertyDetailParameters.Add("UpdateDate", propertyDetail.UpdateDate, DbType.Date);
        //        propertyDetailParameters.Add("CreateBy", propertyDetail.CreateBy, DbType.String);
        //        propertyDetailParameters.Add("UpdateBy", propertyDetail.UpdateBy, DbType.String);
        //        //propertyDetailParameters.Add("Images", propertyDetail.Images, DbType.String);
        //        propertyDetailParameters.Add("Service", propertyDetail.Service, DbType.String);
        //        propertyDetailParameters.Add("VerifyBy", propertyDetail.VerifyBy, DbType.String);
        //        propertyDetailParameters.Add("VerifyDate", propertyDetail.VerifyDate, DbType.Date);

        //        return await connection.ExecuteAsync(propertyDetailQuery, propertyDetailParameters);

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message, ex);
        //    }
        //}

        Task<int> IPropertyRepository.DeletePropertyAsync(string id)
        {
            throw new NotImplementedException();
        }

        

        public async Task<IEnumerable<Property>> GetPropertyForHomePage()
        {
            try
            {
                var query = "SELECT TOP 10 * FROM dbo.Property";
                
                using var connection = CreateConnection();
                return await connection.QueryAsync<Property>(query);
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

        public Task<int> CreatePropertyDetailAsync(PropertyDetail detail)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetLatestPropertyIdAsync()
        {
            throw new NotImplementedException();
        }

        public Task<string> GetLatestPropertyDetailIdAsync()
        {
            throw new NotImplementedException();
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
                var query = "SELECT TOP 10 * FROM dbo.Project";

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
                var query = "SELECT * " +
                    "FROM UserDetail " +
                    "WHERE UserId = @value";
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
    }
}
