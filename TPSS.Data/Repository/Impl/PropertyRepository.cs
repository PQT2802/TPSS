
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
                var query = "INSERT INTO [Property] (PropertyID, ProjectID, PropertyTitle, Price, Image, Area, Province, City, District, Ward, Street, IsDelete) " +
                    "VALUES(@PropertyID, @ProjectID, @PropertyTitle, @Price, @Image, @Area, @Province, @City, @District, @Ward, @Street, @IsDelete)";
                
                var parameter = new DynamicParameters();
                parameter.Add("PropertyID", property.PropertyId, DbType.String);
                parameter.Add("ProjectID", property.ProjectId, DbType.String);
                parameter.Add("PropertyTitle", property.PropertyTitle, DbType.String);
                parameter.Add("Price", property.Price, DbType.Double);


                using var connection = CreateConnection();
                return await connection.ExecuteAsync(query, parameter);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        Task<int> IPropertyRepository.DeletePropertyAsync(string id)
        {
            throw new NotImplementedException();
        }

        

        public async Task<IEnumerable<Property>> GetPropertyForHomePage()
        {
            try
            {
                var query = "SELECT TOP 3 * FROM dbo.Property";
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
                    "FROM [dbo].[Property] AS P " +
                    "JOIN [dbo].[PropertyDetail] AS PD ON P.[PropertyID] = PD.[PropertyID] " +
                    "WHERE P.[PropertyID] = @PropertyID;";

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

        public async Task<IEnumerable<Property>> GetRelatedPropertiesAsync(string city)
        {
            try
            {
                var query = "SELECT TOP 5 * FROM dbo.Property" +
                    "Where City = @City";
                var parameter = new DynamicParameters();
                parameter.Add("City", city, DbType.String);
                using var connection = CreateConnection();
                return await connection.QueryAsync<Property>(query);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
    }
}
