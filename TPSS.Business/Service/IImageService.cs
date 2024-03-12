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
        public Task<List<string>> UploadImagesAsync(IFormFileCollection images, string folderName, string typeID);
        public Task<List<string>> DeleteImagesAsync(List<string> URLs);
        public Task<string> UploadAvatarAsync(IFormFile image, string folderName, string typeID);

    }
}
