
using Microsoft.AspNetCore.Http;
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
        private readonly IConfiguration _configuration;
        public PropertyService(IPropertyRepository propertyRepository, IConfiguration configuration)
        {
            _propertyRepository = propertyRepository;
        }

        

        //Tao property + propertyDetail
        //public async Task<dynamic> CreatePropertyAsync(PropertyDTO propertyDTO)
        //{
        //    try
        //    {
        //        List<Error> Errors = new List<Error>();
        //        Property property = new Property();
                
        //        // check null tu dong
        //        Type type = propertyDTO.GetType();

        //        foreach (var check in type.GetProperties())
        //        {
        //            if(check.GetValue(propertyDTO) == null && 
        //                check.Name != nameof(propertyDTO.PropertyId)
        //                )
        //            {
        //                Errors.Add(Result.CreateError($"{check.Name}.NotFound",$"{check.Name} cannot be null."));
        //            }
        //        }

        //        if (Errors.Count > 0) 
        //        {
        //            return Result.Failures(Errors);


        //        }
        //        property.PropertyId = await AutoGeneratePropertyId();
        //        property.ProjectId = propertyDTO.ProjectId;
        //        property.PropertyTitle = propertyDTO.PropertyTitle;
        //        property.Price = propertyDTO.Price;
        //        property.Image = propertyDTO.Image;
        //        property.Area = propertyDTO.Area;
        //        property.Province = propertyDTO.Province;
        //        property.City = propertyDTO.City;
        //        property.Ward = propertyDTO.Ward;
        //        property.Street = propertyDTO.Street;
        //        property.IsDelete = false;

                


        //        int result1 = await _propertyRepository.CreatePropertyAsync(property);
        //        if(result1 == 1)
        //        {
        //            PropertyDetail detail = new PropertyDetail();

        //            detail.PropertyId = property.PropertyId;
        //            detail.PropertyDetailId = await AutoGeneratePropertyDetailId();
        //            detail.OwnerId = propertyDTO.OwnerID;
        //            detail.PropertyTitle = propertyDTO.PropertyTitle;
        //            detail.Description = propertyDTO.Description;

        //            DateTime currentDate = DateTime.Now; // hoặc DateTime.Now nếu bạn muốn sử dụng múi giờ địa phương
        //            detail.CreateDate = currentDate;
        //            detail.UpdateDate = currentDate;

        //            detail.CreateBy = propertyDTO.OwnerID;
        //            detail.UpdateBy = propertyDTO.OwnerID;
        //            detail.Service = propertyDTO.Service;
        //            detail.Verify = false;
        //            detail.VerifyBy = null;
        //            detail.VerifyDate = null;

        //            int result2 = await _propertyRepository.CreatePropertyDetailAsync(detail);
        //        }
        //        return result1;
            
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message, e);
        //    }
        //}


        public async Task<int> DeletePropertyAsync(string id)
        {
            try
            {
                int result = await _propertyRepository.DeletePropertyAsync(id);
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
                Property result = await _propertyRepository.GetPropertyByIdAsync(id);
                return result;
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

        public async Task<IEnumerable<Property>> GetPropertyForHomePage()
        {
            try
            {

                IEnumerable<Property> result = await _propertyRepository.GetPropertyForHomePage();
                return result;
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
            string latestPropertyId = await _propertyRepository.GetLatestPropertyDetailIdAsync();
            if (latestPropertyId.IsNullOrEmpty())
            {
                newPropertyid = "PD00000000";
            }
            else
            {
                int numericpart = int.Parse(latestPropertyId.Substring(2));
                int newnumericpart = numericpart + 1;

                newPropertyid = $"PD{newnumericpart:d8}";
            }
            return newPropertyid;
        }

        public async Task<PropertyDetailWithRelatedProperties> GetPropertyDetailWithRelatedProperties(string propertyID)
        {
            try
            {

                var propertyDetail = await _propertyRepository.GetPropertyByIdAsync(propertyID);

                var owner = await _propertyRepository.GetOwnerByIdAsync(propertyDetail.OwnerId);

                var project = await _propertyRepository.GetProjectNameAsync(propertyDetail.ProjectId);
                
                IEnumerable<Property> relatedProperties = null;

                if (propertyDetail.City == null)
                {
                     relatedProperties = await _propertyRepository.GetRelatedPropertiesByProvinceAsync(propertyDetail.Province);
                }
                else
                {
                     relatedProperties = await _propertyRepository.GetRelatedPropertiesByCityAsync(propertyDetail.City);
                }
               
                // Tạo đối tượng chứa thông tin PropertyDetail và danh sách các Property khác
                var result = new PropertyDetailWithRelatedProperties(propertyDetail, relatedProperties,owner,project);

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
    }
}
