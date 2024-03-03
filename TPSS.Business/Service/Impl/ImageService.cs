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

namespace TPSS.Business.Service.Impl
{
    public class ImageService : IImageService
    {
        private readonly FirebaseSetting _firebaseSetting;
        private readonly ILogger<ImageService> _logger;

        public ImageService(IOptions<FirebaseSetting> firebaseSetting, ILogger<ImageService> logger) 
        {
            _firebaseSetting = firebaseSetting.Value;
            _logger = logger;
        }


        /// test ảnh
        public async Task<string> UploadImageToFirebaseStorage(IFormFile thumbnail, string folderName)
        {
            try
            {
                var tokenDescriptor = new Dictionary<string, object>()
                {
                    {"permission", "allow" }
                };
                //update anh
                //gioi han image
                //gioi han size 
                //nen size anh

                //string storageToken = await FirebaseAdmin.Auth.FirebaseAuth.DefaultInstance.CreateCustomTokenAsync(tripId, tokenDescriptor);
                
                var auth = new FirebaseAuthProvider(new FirebaseConfig(_firebaseSetting.ApiKey));



                var token = await auth.SignInWithEmailAndPasswordAsync(_firebaseSetting.Email, _firebaseSetting.Password);

                var uploadTask = new FirebaseStorage(
                                     _firebaseSetting.Bucket,
                                     new FirebaseStorageOptions
                                     {
                                         AuthTokenAsyncFactory = () => Task.FromResult(token.FirebaseToken),
                                         ThrowOnCancel = true,
                                     }).Child("Images").Child(folderName).PutAsync(thumbnail.OpenReadStream());

                var downloadUrl = await uploadTask;


                return downloadUrl.ToString();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace, ex);
                throw;
            }
        }
        public async Task<string> UploadMultipleImagesToFirebaseStorage(IFormFileCollection thumbnails, string folderName)
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

                var downloadUrls = new List<string>();
                foreach (var thumbnail in thumbnails)
                {
                    var uploadTask = storage.Child("Images").Child(folderName).PutAsync(thumbnail.OpenReadStream());
                    var downloadUrl = await uploadTask;
                    downloadUrls.Add(downloadUrl.ToString());
                }

                return string.Join(",", downloadUrls);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace, ex);
                throw;
            }
        }
        /// end test
        
        
        public async Task<string> UploadImagesForProperty(IFormFileCollection images, string propertyID)
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

                var downloadUrls = new List<string>();
                int counter = 1;
                foreach (var image in images)
                {
                    var fileName = $"{propertyID}-{counter:00}";
                    var uploadTask = storage.Child("Images").Child("PropertyDetail").Child(propertyID).Child(fileName).PutAsync(image.OpenReadStream());
                    var downloadUrl = await uploadTask;
                    downloadUrls.Add(downloadUrl.ToString());
                    counter++;
                }

                return string.Join(",", downloadUrls);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace, ex);
                throw;
            }
        }

        public async Task<string> UploadImageToForAvatar(IFormFile image, string userID)
        {
            try
            {
                //var tokenDescriptor = new Dictionary<string, object>()
                //{
                //    {"permission", "allow" }
                //};
                ////update anh
                ////gioi han image
                ////gioi han size 
                ////nen size anh

                //string storageToken = await FirebaseAdmin.Auth.FirebaseAuth.DefaultInstance.CreateCustomTokenAsync(tripId, tokenDescriptor);

                var auth = new FirebaseAuthProvider(new FirebaseConfig(_firebaseSetting.ApiKey));



                var token = await auth.SignInWithEmailAndPasswordAsync(_firebaseSetting.Email, _firebaseSetting.Password);

                var uploadTask = new FirebaseStorage(
                                     _firebaseSetting.Bucket,
                                     new FirebaseStorageOptions
                                     {
                                         AuthTokenAsyncFactory = () => Task.FromResult(token.FirebaseToken),
                                         ThrowOnCancel = true,
                                     }).Child("Images").Child("Avatar").Child(userID).PutAsync(image.OpenReadStream());

                var downloadUrl = await uploadTask;


                return downloadUrl.ToString();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace, ex);
                throw;
            }
        }

        public async Task<string> UploadImagesForProjectDetail(IFormFileCollection images, string projectID)
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

                var downloadUrls = new List<string>();
                int counter = 1;
                foreach (var image in images)
                {
                    var fileName = $"{projectID}-{counter:00}";
                    var uploadTask = storage.Child("Images").Child("ProjectDetail").Child(projectID).Child(fileName).PutAsync(image.OpenReadStream());
                    var downloadUrl = await uploadTask;
                    downloadUrls.Add(downloadUrl.ToString());
                    counter++;
                }

                return string.Join(",", downloadUrls);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace, ex);
                throw;
            }
        }
    }
}
