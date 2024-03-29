﻿
using Firebase.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;
using TPSS.Business.Common;
using TPSS.Business.Exceptions.ErrorHandler;
using TPSS.Data.Models.DTO;
using TPSS.Data.Models.Entities;
using TPSS.Data.Repository;
using TPSS.Data.Repository.Impl;

namespace TPSS.Business.Service.Impl
{
    public class PropertyService : IPropertyService
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IAlbumRepository _albumRepository;
        private readonly IImageService _imageService;
        private readonly IConfiguration _configuration;
        public PropertyService(IPropertyRepository propertyRepository, IConfiguration configuration, IImageService imageService, IAlbumRepository albumRepository)
        {
            _propertyRepository = propertyRepository;
            _imageService = imageService;
            _albumRepository = albumRepository;
        }
        public async Task<dynamic> CreatePropertyAsync(PropertyDTO propertyDTO, string userID)
        {
            try
            {
                List<Error> Errors = new List<Error>();

                Property property = new Property();

                if (propertyDTO.Area == null)
                {
                    Errors.Add(CreateErrors.AreaIsEmpty);
                }
                if (string.IsNullOrEmpty(propertyDTO.PropertyTitle))
                {
                    Errors.Add(CreateErrors.PropertyTitleIsEmpty);
                }
                if (propertyDTO.Price == null)
                {
                    Errors.Add(CreateErrors.PriceIsEmpty);
                }
                if (string.IsNullOrEmpty(propertyDTO.City))
                {
                    Errors.Add(CreateErrors.CityIsEmpty);
                }
                if (string.IsNullOrEmpty(propertyDTO.District))
                {
                    Errors.Add(CreateErrors.DistrictIsEmpty);
                }
                if (string.IsNullOrEmpty(propertyDTO.Ward))
                {
                    Errors.Add(CreateErrors.WardIsEmpty);
                }
                if (string.IsNullOrEmpty(propertyDTO.Street))
                {
                    Errors.Add(CreateErrors.StreetIsEmpty);
                }
                if (string.IsNullOrEmpty(propertyDTO.Description))
                {
                    Errors.Add(CreateErrors.DescriptionIsEmpty);
                }
                if (string.IsNullOrEmpty(propertyDTO.Service))
                {
                    propertyDTO.Service = "Normal";
                }
                if (propertyDTO.Images == null)
                {
                    Errors.Add(CreateErrors.ImagesIsEmpty);
                }

                if (Errors.Count > 0)
                {
                    return Result.Failures(Errors);
                }

                property.PropertyId = await AutoGeneratePropertyId();
                property.ProjectId = propertyDTO.ProjectId;
                property.PropertyTitle = propertyDTO.PropertyTitle;
                property.Price = propertyDTO.Price;
                property.Area = propertyDTO.Area;
                property.City = propertyDTO.City;
                property.District = propertyDTO.District;
                property.Ward = propertyDTO.Ward;
                property.Street = propertyDTO.Street;
                property.IsDelete = false;
                int result1 = await _propertyRepository.CreatePropertyAsync(property);
                int result3 = 0;

                if (result1 == 1)
                {
                    PropertyDetail detail = new PropertyDetail();

                    detail.PropertyDetailId = await AutoGeneratePropertyDetailId();
                    detail.PropertyId = property.PropertyId;
                    detail.OwnerId = userID;
                    detail.Description = propertyDTO.Description;

                    DateTime currentDate = DateTime.Now; // hoặc DateTime.Now nếu bạn muốn sử dụng múi giờ địa phương
                    detail.CreateDate = currentDate;
                    detail.UpdateDate = null;
                    detail.UpdateBy = null;
                    detail.Service = propertyDTO.Service;
                    detail.Verify = false;
                    detail.VerifyBy = null;
                    detail.VerifyDate = null;
                    detail.Status = "Waiting";
                    detail.CreateBy = userID;

                    int result2 = await _propertyRepository.CreatePropertyDetailAsync(detail);

                    if (result2 == 1)
                    {
                        
                        result3 = await _albumRepository.CreateAlbumAsync(property.PropertyId, await _imageService.UploadImagesAsync(propertyDTO.Images, "Properties", property.PropertyId));
                    }

                }

                return result3;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }


        public async Task<dynamic> DeletePropertyAsync(string propertyId)
        {
            try
            {
                var result = await _propertyRepository.DeletePropertyAsync(propertyId);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }






            public async Task<dynamic> UpdatePropertyAsync(PropertyDTO propertyDTO, List<string> URLs, string propertyId, string propertyDetailId, string uid)
            {
                try
                {
                    Property property = new Property();
                    property.PropertyId = propertyId;
                    property.ProjectId = propertyDTO.ProjectId;
                    property.PropertyTitle = propertyDTO.PropertyTitle;
                    property.Price = propertyDTO.Price;
                    property.Area = propertyDTO.Area;
                    property.City = propertyDTO.City;
                    property.District = propertyDTO.District;
                    property.Ward = propertyDTO.Ward;
                    property.Street = propertyDTO.Street;
                    property.IsDelete = false;
                    int result1 = await _propertyRepository.UpdatePropertyAsync(property);
                    int result3 = 0;
                    int result4 = 0;

                    if (result1 == 1)
                    {
                        PropertyDetail detail = new PropertyDetail();

                        detail.PropertyDetailId = propertyDetailId;
                        detail.PropertyId = property.PropertyId;
                        detail.OwnerId = uid;
                        detail.Description = propertyDTO.Description;
                        DateTime currentDate = DateTime.Now; // hoặc DateTime.Now nếu bạn muốn sử dụng múi giờ địa phương
                        detail.UpdateDate = currentDate;
                        detail.UpdateBy = uid;

                        int result2 = await _propertyRepository.UpdatePropertyDetailAsync(detail);
                        if (result2 == 1)
                        {
                            if(URLs != null)
                            {
                                List<string> deletedImageIds = await _imageService.DeleteImagesAsync(URLs);
                                result4 = await _albumRepository.DeleteImagesAsync(deletedImageIds);
                            }
                        
                            if(propertyDTO.Images != null)
                            {
                                result3 = await _albumRepository.CreateAlbumAsync(property.PropertyId, await _imageService.UploadImagesAsync(propertyDTO.Images, "Properties", property.PropertyId));

                            }

                        }

                    }

                    return new { ImagesDelete = result4, ImagesAdd = result3 };

            }
                catch(Exception e)
                {
                    throw new Exception(e.Message, e);
                }
            }    

        public async Task<IEnumerable<dynamic>> GetPropertyForHomePage()
        {
            try
            {

                var resultList = await _propertyRepository.GetPropertyForHomePage();

                return resultList;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message, e);
            }
        }

        private async Task<string> AutoGeneratePropertyId()
        {
            string newPropertyid = "";
            string latestPropertyId = await _propertyRepository.GetLatestPropertyIdAsync();
            if (latestPropertyId.IsNullOrEmpty())
            {
                newPropertyid = "PP00000001";
            }
            else
            {
                int numericpart = int.Parse(latestPropertyId.Substring(2));
                int newnumericpart = numericpart + 1;

                newPropertyid = $"PP{newnumericpart:d8}";
            }
            return newPropertyid;
        }

        private async Task<string> AutoGeneratePropertyDetailId()
        {
            string newPropertyid = "";
            string latestPropertyDetailId = await _propertyRepository.GetLatestPropertyDetailIdAsync();
            if (latestPropertyDetailId.IsNullOrEmpty())
            {
                newPropertyid = "PD00000001";
            }
            else
            {
                int numericpart = int.Parse(latestPropertyDetailId.Substring(2));
                int newnumericpart = numericpart + 1;

                newPropertyid = $"PD{newnumericpart:d8}";
            }
            return newPropertyid;
        }

        public async Task<dynamic> GetPropertyByIdAsync(string propertyID)
        {
            try
            {

                var propertyDetail = await _propertyRepository.GetPropertyByIdAsync(propertyID);

                var albumImages = await _albumRepository.GetAlbumByPropertyID(propertyID);

                IEnumerable<dynamic> relatedProperties = null;

                if (propertyDetail.City != null)
                {
                    relatedProperties = await _propertyRepository.GetRelatedPropertiesByCityAsync(propertyDetail.City);
                }
                else
                {
                    relatedProperties = await _propertyRepository.GetRelatedPropertiesByDistrictAsync(propertyDetail.District);
                }

                var result = new 
                {
                    PropertyDetail = propertyDetail,
                    AlbumImages = albumImages,
                    RelatedProperties = relatedProperties
                }; 

                return result;

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
                IEnumerable<Project> result = await _propertyRepository.GetAllProjects();
                return result;
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
                ProjectDetail result = await _propertyRepository.GetProjectDetail(id);
                return result;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message, e);
            }
        }

        public async Task<ProjectDetailWithRelatedProperties> GetProjectDetailWithRelatedProperties(string projectID)
        {
            try
            {
                var projectDetail = await _propertyRepository.GetProjectDetail(projectID);

                IEnumerable<Property> relatedProperties = await _propertyRepository.GetRelatedPropertiesByProjectIDAsync(projectID);

                var result = new ProjectDetailWithRelatedProperties(projectDetail, relatedProperties);

                return result;

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
                IEnumerable<Property> result = await _propertyRepository.GetPropertiesByUserIDAsync(UserID);
                return result;
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
                IEnumerable<Project> result = await _propertyRepository.GetLastestProject();
                return result;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message, e);
            }
        }

        public async Task<IEnumerable<dynamic>> MyProperties(string userID)
        {
            try
            {
                var properties = await _propertyRepository.MyProperties(userID);
                var propertyImages = await _albumRepository.MyPropertiesImages(userID);

                var groupedImages = propertyImages.GroupBy(img => img.PropertyId);

                var result = properties.Select(property =>
                {
                    var propertyId = property.PropertyID.ToString();
                    var imagesGroup = groupedImages.FirstOrDefault(group => group.Key == propertyId);
                    var images = imagesGroup != null ? imagesGroup.ToList() : new List<dynamic>();

                    return new
                    {
                        Property = property,
                        Images = images
                    };
                });

                return result;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message, e);
            }
        }

        public async Task<IEnumerable<dynamic>> GetVerifyPropertiesAsync()
        {
            try
            {
                var properties = await _propertyRepository.GetVerifyPropertiesAsync();
                return properties;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message, e);
            }
        }

        public async Task<IEnumerable<dynamic>> GetWaitingPropertiesAsync()
        {
            try
            {
                var properties = await _propertyRepository.GetWaitingPropertiesAsync();
                return properties;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message, e);
            }
        }

        public async Task<dynamic> VerifyPropertiesAsync(List<string> propertiesID)
        {
            try
            {
                var properties = await _propertyRepository.VerifyPropertiesAsync(propertiesID);
                return properties;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message, e);  
            }
        }

        public async Task<dynamic> AcceptedPropertiesAsync(List<string> propertiesID)
        {
            try
            {
                var properties = await _propertyRepository.AcceptedPropertiesAsync(propertiesID);
                return properties;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message, e);
            }
        }

        // test
        public async Task<dynamic> CreatePropertyTESTAsync(PropertyDTO propertyDTO, string uid)
        {
            try
            {
                List<Error> Errors = new List<Error>();

                Property property = new Property();

                if (propertyDTO.Area == null)
                {
                    Errors.Add(CreateErrors.AreaIsEmpty);
                }
                if (string.IsNullOrEmpty(propertyDTO.PropertyTitle))
                {
                    Errors.Add(CreateErrors.PropertyTitleIsEmpty);
                }
                if (propertyDTO.Price == null)
                {
                    Errors.Add(CreateErrors.PriceIsEmpty);
                }
                if (string.IsNullOrEmpty(propertyDTO.City))
                {
                    Errors.Add(CreateErrors.CityIsEmpty);
                }
                if (string.IsNullOrEmpty(propertyDTO.District))
                {
                    Errors.Add(CreateErrors.DistrictIsEmpty);
                }
                if (string.IsNullOrEmpty(propertyDTO.Ward))
                {
                    Errors.Add(CreateErrors.WardIsEmpty);
                }
                if (string.IsNullOrEmpty(propertyDTO.Street))
                {
                    Errors.Add(CreateErrors.StreetIsEmpty);
                }
                if (string.IsNullOrEmpty(propertyDTO.Description))
                {
                    Errors.Add(CreateErrors.DescriptionIsEmpty);
                }
                if (string.IsNullOrEmpty(propertyDTO.Service))
                {
                    propertyDTO.Service = "Normal";
                }
                if (propertyDTO.Images == null)
                {
                    Errors.Add(CreateErrors.ImagesIsEmpty);
                }

                if (Errors.Count > 0)
                {
                    return Result.Failures(Errors);
                }

                property.PropertyId = await AutoGeneratePropertyId();
                property.ProjectId = propertyDTO.ProjectId;
                property.PropertyTitle = propertyDTO.PropertyTitle;
                property.Price = propertyDTO.Price;
                property.Area = propertyDTO.Area;
                property.City = propertyDTO.City;
                property.District = propertyDTO.District;
                property.Ward = propertyDTO.Ward;
                property.Street = propertyDTO.Street;
                property.IsDelete = false;
                int result1 = await _propertyRepository.CreatePropertyAsync(property);
                int result3 = 0;

                if (result1 == 1)
                {
                    PropertyDetail detail = new PropertyDetail();

                    detail.PropertyDetailId = await AutoGeneratePropertyDetailId();
                    detail.PropertyId = property.PropertyId;
                    detail.OwnerId = uid;
                    detail.Description = propertyDTO.Description;

                    DateTime currentDate = DateTime.Now; // hoặc DateTime.Now nếu bạn muốn sử dụng múi giờ địa phương
                    detail.CreateDate = currentDate;
                    detail.UpdateDate = null;
                    detail.UpdateBy = null;
                    detail.Service = propertyDTO.Service;
                    detail.Verify = false;
                    detail.VerifyBy = null;
                    detail.VerifyDate = null;
                    detail.Status = "Waiting";
                    detail.CreateBy = uid;

                    int result2 = await _propertyRepository.CreatePropertyDetailAsync(detail);

                    if (result2 == 1)
                    {

                        result3 = await _albumRepository.CreateAlbumAsync(property.PropertyId, await _imageService.UploadImagesAsync(propertyDTO.Images, "Properties", property.PropertyId));
                    }

                }

                return result3;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }


    }
}
