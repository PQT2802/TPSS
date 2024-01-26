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

        public async Task<int> CreatePropertyAsync(Property property)
        {
            try
            {
                string query = "INSERT INTO [Property](PropertyID, ProjectID, PropertyTitle, Price, Image, Area, Province, City, District, Ward, Street, IsDelete) " +
                    "VALUES (@PropertyID, @ProjectID, @PropertyTitle, @Price, @Image, @Area, @Province, @City, @District, @Ward, @Street, @IsDelete)";

                using var connection = CreateConnection();
                var parameter = new DynamicParameters();
                parameter.Add("PropertyID",property.PropertyId,DbType.String);
                parameter.Add("ProjectID", property.ProjectId, DbType.String);
                parameter.Add("PropertyTitle", property.PropertyTitle, DbType.String);
                parameter.Add("Price", property.Price, DbType.Double);
                parameter.Add("Image", property.Image, DbType.String);
                parameter.Add("Area", property.Area, DbType.Double);
                parameter.Add("Province", property.Province, DbType.String);
                parameter.Add("City", property.City, DbType.String);
                parameter.Add("District", property.District, DbType.String);
                parameter.Add("Ward", property.Ward, DbType.String);
                parameter.Add("Street", property.Street, DbType.String);
                parameter.Add("IsDelete", property.IsDelete, DbType.Boolean);

                return await connection.ExecuteAsync(query,parameter);


            }
            catch (Exception e)
            {

                throw new Exception(e.Message, e);
            }
        }


        public async Task<int> DeletePropertyAsync(string id)
        {
            try
            {
                var query = "UPDATE [Property] " +
                    "SET IsDelete = true" +
                    "WHERE UserId = @PropertyID";

                using var connection = CreateConnection();
                var parameter = new DynamicParameters();
                parameter.Add("PropertyID", id, DbType.String);

                return await connection.ExecuteAsync(query, parameter);


            }
            catch (Exception e)
            {

                throw new Exception(e.Message, e);
            }
        }

        public async Task<Property> GetPropertyByIdAsync(string id)
        {
            try
            {
                var query = "SELECT *" +
                    "FROM [Property]" +
                    "WHERE PropertyID = @PropertyID";
                var parameter = new DynamicParameters();
                parameter.Add("PropertyID", id, DbType.String);
                using var connection = CreateConnection();
                return await connection.QuerySingleAsync<Property>(query, parameter);


            }
            catch (Exception e)
            {

                throw new Exception(e.Message, e);
            }
        }

        public async Task<int> UpdatePropertyAsync(Property property)
        {
            //update soon
            throw new NotImplementedException();
        }
    }
}
