using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPSS.Data.Models.DTO;

namespace TPSS.Business.Service
{
    public interface IImageService
    {
        public Task<string> UploadImageToFirebaseStorage(IFormFile image, string folderName);
        public Task<string> UploadMultipleImagesToFirebaseStorage(IFormFileCollection thumbnails, string folderName);
        public Task<List<string>> UploadImagesForProperty(IFormFileCollection images, string propertyID);
        public Task<List<string>> LinkFolderCheck(IFormFileCollection images, string propertyID);
        public Task<List<string>> UploadImagesForPropertyTest(IFormFileCollection images, string propertyID);
        public Task<dynamic> DeleteImagePropertyAsync(string url);
        public Task<List<string>> UploadImagesForProjectDetail(IFormFileCollection images, string projectID, string latestImageID);
    }
}
