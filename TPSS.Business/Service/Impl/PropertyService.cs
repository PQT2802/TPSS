﻿
using Firebase.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private readonly IImageService _imageService;
        private readonly IConfiguration _configuration;
        public PropertyService(IPropertyRepository propertyRepository, IConfiguration configuration, IImageService imageService)
        {
            _propertyRepository = propertyRepository;
            _imageService = imageService;
        }
        public async Task<dynamic> CreatePropertyAsync(PropertyDTO propertyDTO, string userID)
        {
            try
            {
                List<Error> Errors = new List<Error>();
                Property property = new Property();

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
                    detail.UpdateDate = null;

                    DateTime currentDate = DateTime.Now; // hoặc DateTime.Now nếu bạn muốn sử dụng múi giờ địa phương
                    detail.CreateDate = currentDate;

                    detail.UpdateBy = userID;
                    detail.Service = propertyDTO.Service;
                    detail.Verify = false;
                    detail.VerifyBy = null;
                    detail.VerifyDate = null;
                    detail.Status = "Waiting";

                    int result2 = await _propertyRepository.CreatePropertyDetailAsync(detail);
                }
                return result1;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }


        public async Task<dynamic> DeletePropertyAsync(string id)
        {
            try
            {
                var result = await _propertyRepository.DeletePropertyAsync(id);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }


        public async Task<Property> GetPropertyByIdAsync(string id)
        {
            try
            {
                //Property result = await _propertyRepository.GetPropertyByIdAsync(id);
                //return result;
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }



        public async Task<dynamic> UpdatePropertyAsync(PropertyDTO property)
        {
            //update soon
            throw new NotImplementedException();
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
                newPropertyid = "PP00000000";
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
                newPropertyid = "PD00000000";
            }
            else
            {
                int numericpart = int.Parse(latestPropertyDetailId.Substring(2));
                int newnumericpart = numericpart + 1;

                newPropertyid = $"PD{newnumericpart:d8}";
            }
            return newPropertyid;
        }

        //public async Task<PropertyDetailWithRelatedProperties> GetPropertyDetailWithRelatedProperties(string propertyID)
        //{
        //    try
        //    {

        //        var propertyDetail = await _propertyRepository.GetPropertyByIdAsync(propertyID);

        //        var owner = await _propertyRepository.GetOwnerByIdAsync(propertyDetail.OwnerId);


        //        var project = await _propertyRepository.GetProjectNameAsync(propertyDetail.ProjectId);

                //var project = await _propertyRepository.GetProjectNameAsync(propertyDetail.ProjectId);

                
        //        IEnumerable<Property> relatedProperties = null;

        //        if (propertyDetail.City != null)
        //        {
        //            relatedProperties = await _propertyRepository.GetRelatedPropertiesByCityAsync(propertyDetail.City);
        //        }
               
        //        // Tạo đối tượng chứa thông tin PropertyDetail và danh sách các Property khác
        //        var result = new PropertyDetailWithRelatedProperties(propertyDetail, relatedProperties,owner,project);

        //        return result;

                //if (propertyDetail.City != null)
                //{
                //    relatedProperties = await _propertyRepository.GetRelatedPropertiesByCityAsync(propertyDetail.City);
                //}

                // Tạo đối tượng chứa thông tin PropertyDetail và danh sách các Property khác
                //var result = new PropertyDetailWithRelatedProperties(propertyDetail, relatedProperties,owner,project);

                //return result;



        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message, e);
        //    }
        //}

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
                var propertyImages = await _propertyRepository.MyPropertiesImages(userID);

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



        // test
        public async Task<dynamic> CreatePropertyTESTAsync(PropertyDTO propertyDTO, string uid)
        {
            try
            {
                List<Error> Errors = new List<Error>();

                Property property = new Property();


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
                    detail.UpdateDate = null;

                    DateTime currentDate = DateTime.Now; // hoặc DateTime.Now nếu bạn muốn sử dụng múi giờ địa phương
                    detail.CreateDate = currentDate;

                    detail.UpdateBy = property.PropertyId;
                    detail.Service = propertyDTO.Service;
                    detail.Verify = false;
                    detail.VerifyBy = null;
                    detail.VerifyDate = null;
                    detail.Status = "Normal";

                    int result2 = await _propertyRepository.CreatePropertyDetailAsync(detail);

                    if (result2 == 1)
                    {
                        result3 = await _propertyRepository .CreateAlbumAsync(property.PropertyId, await _imageService.UploadImagesForPropertyTest(propertyDTO.Images, property.PropertyId));
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
