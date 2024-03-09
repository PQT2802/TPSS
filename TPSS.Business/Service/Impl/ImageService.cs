﻿using Microsoft.AspNetCore.Http;
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
        public async Task<dynamic> DeleteImagePropertyAsync(string url)
        {
            try
            {
                string pattern = @"/Images%2F([^%]+)%2F([^%]+)%2F([^?]+)";
                Match match = Regex.Match(url, pattern);
                string folder = match.Groups[1].Value;
                string propertyId = match.Groups[2].Value;
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
                await storage.Child("Images").Child(folder).Child(propertyId).Child(imageId).DeleteAsync();
                return new { Message = "Image deleted successfully" }; 


            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace, ex);
                throw;
            }
        }



        /// end test


        public async Task<List<string>> UploadImagesForProperty(IFormFileCollection images, string propertyID)
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

                return downloadUrls;
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

        public async Task<List<string>> UploadImagesForProjectDetail(IFormFileCollection images, string projectID, string latestImageID)
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
                int nextImageNumber = int.Parse(latestImageID.Substring(2)) + 1;
                foreach (var image in images)
                {
                    var fileName = $"IM{nextImageNumber:00000000}";
                    var uploadTask = storage.Child("Images").Child("ProjectDetail").Child(projectID).Child(fileName).PutAsync(image.OpenReadStream());
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

        public async Task<List<string>> LinkFolderCheck(IFormFileCollection images, string propertyID)
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



                // Return the folder download URL
                return downloadUrls;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace, ex);
                throw;
            }
        }
        public async Task<List<string>> UploadImagesForPropertyTest(IFormFileCollection images, string propertyID)
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

                return downloadUrls;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace, ex);
                throw;
            }
        }
    }
}
