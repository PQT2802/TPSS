using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPSS.Business.Service
{
    public interface IImageService
    {
        public Task<string> UploadImageToFirebaseStorage(IFormFile image, string folderName);
        public Task<string> UploadMultipleImagesToFirebaseStorage(List<IFormFile> files);
        public Task<string> UploadImagesForProperty(List<IFormFile> images, string propertyID);
    }
}
