
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

        public async Task<int> DeletePropertyAsync(string id)
        {
            throw new NotImplementedException();
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
                    "WHERE A.ImageDescription = 'HomePage';";   

                using var connection = CreateConnection();
                return await connection.QueryAsync<dynamic>(query);

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
                var query = "INSERT INTO [PropertyDetail] (PropertyDetailID, PropertyID, OwnerID, Description, CreateDate, UpdateDate, UpdateBy, Service, VerifyBy, VerifyDate, Verify, Status) " +
                    "VALUES(@PropertyDetailID, @PropertyID, @OwnerID, @Description, @CreateDate, @UpdateDate, @UpdateBy, @Service, @VerifyBy, @VerifyDate, @Verify, @Status)";

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

        private async Task<string> GetLatestImageIdAsync()
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


        public async Task<int> CreateAlbumAsync(string propertyId, List<string> images)
        {
            try
            {
                string latestImageId = await GetLatestImageIdAsync();
                int newImageNumericPart = 1 + (latestImageId.IsNullOrEmpty() ? 0 : int.Parse(latestImageId.Substring(2)));


                using var connection = CreateConnection() as SqlConnection;

                await connection.OpenAsync();

                using var bulkCopy = new SqlBulkCopy(connection);

                bulkCopy.DestinationTableName = "Album";
                bulkCopy.WriteToServerAsync(PrepareDataTable(propertyId, images, newImageNumericPart));

                return images.Count; // Assuming all inserts were successful
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        private DataTable PrepareDataTable(string propertyId, List<string> images, int startingImageId)
        {
            var dataTable = new DataTable();
            dataTable.Columns.Add("ImageId", typeof(string));
            dataTable.Columns.Add("PropertyId", typeof(string));
            dataTable.Columns.Add("Image", typeof(string));
            dataTable.Columns.Add("ImageDescription", typeof(string)); // Adjust based on your needs

            for (int i = 0; i < images.Count; i++)
            {
                string imageId = $"IM{startingImageId++:d8}";
                string description = i == 0 ? "HomePage" : "DetailPage";
                dataTable.Rows.Add(imageId, propertyId, images[i], description); // Update description as needed
            }

            return dataTable;
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
                    "WHERE A.ImageDescription = 'HomePage';";

                using var connection = CreateConnection();
                return await connection.QueryAsync<dynamic>(query);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task<IEnumerable<dynamic>> MyPropertiesImages(string userID)
        {
            try
            {
                var query = "SELECT A.[ImageId], A.[PropertyId], A.[Image] " +
                    "FROM [dbo].[Album] A " +
                    "JOIN [dbo].[Property] P ON A.[PropertyId] = P.[PropertyID] " +
                    "JOIN [dbo].[PropertyDetail] PD ON P.[PropertyID] = PD.[PropertyID] " +
                    "WHERE A.[ImageDescription] = 'DetailPage' AND PD.[OwnerID] = @UserID;";

                var parameter = new DynamicParameters();
                parameter.Add("UserID", userID, DbType.String);
                using var connection = CreateConnection();
                
                return await connection.QueryAsync<dynamic>(query,parameter);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
    }
}
