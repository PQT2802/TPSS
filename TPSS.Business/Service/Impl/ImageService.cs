using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPSS.Business.SettingObject;
using TPSS.Data.Repository;
using Firebase.Auth;
using Firebase.Storage;
using Azure.Core;
using static System.Net.Mime.MediaTypeNames;
using System.Text.RegularExpressions;
using Microsoft.IdentityModel.Tokens;
using TPSS.Data.Repository.Impl;

namespace TPSS.Business.Service.Impl
{
    public class ImageService : IImageService
    {
        private readonly FirebaseSetting _firebaseSetting;
        private readonly IAlbumRepository _albumRepository;
        private readonly ILogger<ImageService> _logger;

        public ImageService(IOptions<FirebaseSetting> firebaseSetting, ILogger<ImageService> logger, IAlbumRepository AlbumRepository) 
        {
            _firebaseSetting = firebaseSetting.Value;
            _logger = logger;
            _albumRepository = AlbumRepository;
        }


        public async Task<List<string>> DeleteImagesAsync(List<string> URLs)
        {
            try
            {
                string pattern = @"/Images%2F([^%]+)%2F([^%]+)%2F([^?]+)";

                List<string> deletedImageIds = new List<string>();

                foreach (string url in URLs)
                {

                    Match match = Regex.Match(url, pattern);
                    string folder = match.Groups[1].Value;
                    string type = match.Groups[2].Value;
                    string imageId = match.Groups[3].Value;



                    // Get Firebase authentication token
                    var tokenDescriptor = new Dictionary<string, object>()
                        {
                            {"permission", "allow" }
                        };

                    var auth = new FirebaseAuthProvider(new FirebaseConfig(_firebaseSetting.ApiKey));
                    var token = await auth.SignInWithEmailAndPasswordAsync(_firebaseSetting.Email, _firebaseSetting.Password);

                    // Create a new FirebaseStorage instance with the token
                    var storage = new FirebaseStorage(
                      _firebaseSetting.Bucket,
                      new FirebaseStorageOptions
                      {
                          AuthTokenAsyncFactory = () => Task.FromResult(token.FirebaseToken),
                          ThrowOnCancel = true
                      });

                    //string fileName = imageName;
                    //string newGuid = Guid.NewGuid().ToString();
                    await storage.Child("Images").Child(folder).Child(type).Child(imageId).DeleteAsync();

                    deletedImageIds.Add(imageId);
                }

                return deletedImageIds;


            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace, ex);
                throw;
            }
        }

        private async Task<string> GetLatestImageIdAsync()
        {
            
            string latestImageId = await _albumRepository.GetLatestImageIdAsync();
            
            return latestImageId;
        }

        public async Task<List<string>> UploadImagesAsync(IFormFileCollection images, string folderName, string typeID)
        {
            try
            {
                var tokenDescriptor = new Dictionary<string, object>()
                {
                    {"permission", "allow" }
                };

                var auth = new FirebaseAuthProvider(new FirebaseConfig(_firebaseSetting.ApiKey));
                var token = await auth.SignInWithEmailAndPasswordAsync(_firebaseSetting.Email, _firebaseSetting.Password);

                var storage = new FirebaseStorage(
                    _firebaseSetting.Bucket,
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(token.FirebaseToken),
                        ThrowOnCancel = true,
                    });
                string latestImageID = await GetLatestImageIdAsync();
                if(latestImageID == null)
                {
                    latestImageID = "IM00000000";
                }
                var downloadUrls = new List<string>();
                int nextImageNumber = int.Parse(latestImageID.Substring(2)) + 1;
                foreach (var image in images)
                {
                    var fileName = $"IM{nextImageNumber:00000000}";
                    var uploadTask = storage.Child("Images").Child(folderName).Child(typeID).Child(fileName).PutAsync(image.OpenReadStream());
                    var downloadUrl = await uploadTask;
                    downloadUrls.Add(downloadUrl.ToString());
                    nextImageNumber++;
                }

                return downloadUrls;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace, ex);
                throw;
            }
        }


        public async Task<string> UploadAvatarAsync(IFormFile image, string folderName, string typeID)
        {
            try
            {
                var tokenDescriptor = new Dictionary<string, object>()
                {
                     { "permission", "allow" }
                };

                var auth = new FirebaseAuthProvider(new FirebaseConfig(_firebaseSetting.ApiKey));
                var token = await auth.SignInWithEmailAndPasswordAsync(_firebaseSetting.Email, _firebaseSetting.Password);

                var storage = new FirebaseStorage(
                  _firebaseSetting.Bucket,
                  new FirebaseStorageOptions
                  {
                      AuthTokenAsyncFactory = () => Task.FromResult(token.FirebaseToken),
                      ThrowOnCancel = true
                  });

                var uploadTask = storage.Child("Images").Child(folderName).Child(typeID).Child(typeID).PutAsync(image.OpenReadStream());
                var downloadUrl = await uploadTask;

                return downloadUrl.ToString();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace, ex);
                throw;
            }
        }


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    }
}
