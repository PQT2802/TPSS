using Dapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPSS.Data.Helper;

namespace TPSS.Data.Repository.Impl
{
    public class PropertyRepository : BaseRepository, IPropertyRepository
    {
        public PropertyRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<int> CreatePropertyAsync(Models.Entities.Property property)
        {
            try
            {
                string sql = "INSERT INTO [Property](PropertyID, ProjectID, PropertyTitle, Price, Image, Area, Province, City, District, Ward, Street, IsDelete) " +
                    "VALUES (@PropertyID, @ProjectID, @PropertyTitle, @Price, @Image, @Area, @Province, @City, @District, @Ward, @Street, @IsDelete)";

                using var connection = CreateConnection();
                var parameter = new DynamicParameters();
                parameter.Add("PropertyID",property.ProjectId,DbType.String);

            }
            catch
            {

            }
        }

        Task<int> IPropertyRepository.DeletePropertyAsync(string id)
        {
            throw new NotImplementedException();
        }

        Task<Models.Entities.Property> IPropertyRepository.GetPropertyByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        Task<int> IPropertyRepository.UpdatePropertyAsync(Models.Entities.Property property)
        {
            throw new NotImplementedException();
        }
    }
}
