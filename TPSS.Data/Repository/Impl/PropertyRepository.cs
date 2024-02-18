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
    public class PropertyRepository : BaseRepository, IPropertyRepository
    {
        public PropertyRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<int> CreatePropertyAsync(Property property, PropertyDetail propertyDetail)
        {
            try
            {
                
                var propertyQuery = "INSERT INTO Property (PropertyId, ProjectId, PropertyTitle, Price, Image, Area, Province, City, District, Ward, Street, IsDelete) " +
                                    "VALUES(@PropertyId, @ProjectId, @PropertyTitle, @Price, @Image, @Area, @Province, @City, @District, @Ward, @Street, @IsDelete)";
                var propertyParameters = new DynamicParameters();
                propertyParameters.Add("PropertyId", property.PropertyId, DbType.String);
                propertyParameters.Add("ProjectId", property.ProjectId, DbType.String);
                propertyParameters.Add("PropertyTitle", property.PropertyTitle, DbType.String);
                propertyParameters.Add("Price", property.Price, DbType.Double);
                propertyParameters.Add("Image", property.Image, DbType.String);
                propertyParameters.Add("Area", property.Area, DbType.Double);
                propertyParameters.Add("Province", property.Province, DbType.String);
                propertyParameters.Add("City", property.City, DbType.String);
                propertyParameters.Add("District", property.District, DbType.String);
                propertyParameters.Add("Ward", property.Ward, DbType.String);
                propertyParameters.Add("Street", property.Street, DbType.String);
                propertyParameters.Add("IsDelete", property.IsDelete, DbType.Boolean);

                using var connection = CreateConnection();
                await connection.ExecuteAsync(propertyQuery, propertyParameters);

                
                var propertyDetailQuery = "INSERT INTO PropertyDetail (PropertyDetailId, PropertyId, OwnerId, PropertyTitle, Description, CreateDate, UpdateDate, CreateBy, UpdateBy, Images, Service, VerifyBy, VerifyDate) " +
                                          "VALUES(@PropertyDetailId, @PropertyId, @OwnerId, @PropertyTitle, @Description, @CreateDate, @UpdateDate, @CreateBy, @UpdateBy, @Images, @Service, @VerifyBy, @VerifyDate)";
                var propertyDetailParameters = new DynamicParameters();
                propertyDetailParameters.Add("PropertyDetailId", propertyDetail.PropertyDetailId, DbType.String);
                propertyDetailParameters.Add("PropertyId", propertyDetail.PropertyId, DbType.String);
                propertyDetailParameters.Add("OwnerId", propertyDetail.OwnerId, DbType.String);
                propertyDetailParameters.Add("PropertyTitle", propertyDetail.PropertyTitle, DbType.String);
                propertyDetailParameters.Add("Description", propertyDetail.Description, DbType.String);
                propertyDetailParameters.Add("CreateDate", propertyDetail.CreateDate, DbType.Date);
                propertyDetailParameters.Add("UpdateDate", propertyDetail.UpdateDate, DbType.Date);
                propertyDetailParameters.Add("CreateBy", propertyDetail.CreateBy, DbType.String);
                propertyDetailParameters.Add("UpdateBy", propertyDetail.UpdateBy, DbType.String);
                propertyDetailParameters.Add("Images", propertyDetail.Images, DbType.String);
                propertyDetailParameters.Add("Service", propertyDetail.Service, DbType.String);
                propertyDetailParameters.Add("VerifyBy", propertyDetail.VerifyBy, DbType.String);
                propertyDetailParameters.Add("VerifyDate", propertyDetail.VerifyDate, DbType.Date);

                return await connection.ExecuteAsync(propertyDetailQuery, propertyDetailParameters);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        Task<int> IPropertyRepository.DeletePropertyByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetLatestPropertyIdAsync()
        {
            try
            {
                var query = "SELECT TOP 1 PropertyId " +
                            "FROM Property " +
                            "ORDER BY " +
                            "CAST(SUBSTRING(PropertyId, 8, LEN(PropertyId)) AS INT) DESC, " +
                            "PropertyId DESC";
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
                var query = "SELECT TOP 1 PropertyDetailId " +
                            "FROM PropertyDetail " +
                            "ORDER BY " +
                            "CAST(SUBSTRING(PropertyDetailId, 8, LEN(PropertyDetailId)) AS INT) DESC, " +
                            "PropertyDetailId DESC";
                using var connection = CreateConnection();
                return await connection.QuerySingleOrDefaultAsync<string>(query);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }


        public async Task<Property> GetPropertyByIDAsync(string id)
        {
            try
            {
                var query = "SELECT * FROM Property WHERE PropertyId = @PropertyId";
                var parameters = new { PropertyId = id };
                using var connection = CreateConnection();
                return await connection.QuerySingleOrDefaultAsync<Property>(query, parameters);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        Task<Property> IPropertyRepository.SearchPropertyAsync(double price1, double price2, double Area, string Province, string City)
        {
            throw new NotImplementedException();
        }

        Task<int> IPropertyRepository.UpdatePropertyAsync(Property property)
        {
            throw new NotImplementedException();
        }

        public async Task<PropertyDetail> GetPropertyDetailByIDAsync(string id)
        {
            try
            {
                var query = "SELECT * FROM PropertyDetail WHERE PropertyId = @PropertyId";
                var parameters = new { PropertyId = id };
                using var connection = CreateConnection();
                return await connection.QuerySingleOrDefaultAsync<PropertyDetail>(query, parameters);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
    }
}
