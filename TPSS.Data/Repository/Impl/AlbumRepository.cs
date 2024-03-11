using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPSS.Data.Helper;

namespace TPSS.Data.Repository.Impl
{
    public class AlbumRepository : BaseRepository, IAlbumRepository
    {
        public AlbumRepository(IConfiguration configuration) : base(configuration)
        {
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
        public async Task<int> DeleteImagesAsync(List<string> imageIds)
        {
            try
            {
                using var connection = CreateConnection() as SqlConnection;
                await connection.OpenAsync();

                foreach (string imageId in imageIds)
                {
                    // Modify the query based on your database schema and relationships
                    string deleteQuery = "DELETE FROM Album WHERE ImageId = @ImageId";

                    // Assuming your connection is open and you have a proper implementation of CreateConnection method
                    await connection.ExecuteAsync(deleteQuery, new { ImageId = imageId });
                }

                return imageIds.Count; 
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
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

            bool isFirstImage = CheckForHomePageImage(propertyId);

            for (int i = 0; i < images.Count; i++)
            {
                string imageId = $"IM{startingImageId++:d8}";
                string description = isFirstImage ? "HomePage" : $"DetailPage";
                dataTable.Rows.Add(imageId, propertyId, images[i], description); // Update description as needed
                isFirstImage = false;
            }

            return dataTable;
        }
        private bool CheckForHomePageImage(string propertyId)
        {
            using (var connection = CreateConnection() as SqlConnection)
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM Album WHERE PropertyId = @PropertyId AND ImageDescription = 'HomePage'";

                // Assuming your connection is open and you have a proper implementation of CreateConnection method

                int count = connection.ExecuteScalar<int>(query, new { PropertyId = propertyId });

                return count <= 0;
            }
        }

        public Task<int> UpdateAlbumAsync(string propertyId, List<string> images)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<dynamic>> GetAlbumByPropertyID(string propertyID)
        {
            try
            {
                var query = "SELECT ImageId, [Image], ImageDescription " +
                    "FROM [dbo].[Album] " +
                    "Where PropertyId = @PropertyId;";
                var parameter = new DynamicParameters();
                parameter.Add("PropertyId", propertyID, DbType.String);
                using var connection = CreateConnection();
                return await connection.QueryAsync<dynamic>(query, parameter);
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

                return await connection.QueryAsync<dynamic>(query, parameter);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
    }
}
