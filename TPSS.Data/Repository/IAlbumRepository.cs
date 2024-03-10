using System;

using TPSS.Data.Models.Entities;

namespace TPSS.Data.Repository
{
    public interface IAlbumRepository
    {
        public Task<string> GetLatestImageIdAsync();
        public Task<int> CreateAlbumAsync(string propertyId, List<string> images);
        public Task<int> UpdateAlbumAsync(string propertyId, List<string> images);
        public Task<int> DeleteImagesAsync(List<string> imageIds);
    }
}
