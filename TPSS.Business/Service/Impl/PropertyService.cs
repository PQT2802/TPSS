<<<<<<< HEAD
﻿using System;
=======
﻿using Microsoft.IdentityModel.Tokens;
using System;
>>>>>>> DEV_THANG
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPSS.Data.Models.DTO;
using TPSS.Data.Models.Entities;
using TPSS.Data.Repository;
using TPSS.Data.Repository.Impl;

<<<<<<< HEAD

namespace TPSS.Business.Service.Impl
{
    public class PropertyService : IPropertyService
    {
        private readonly IPropertyRepository _propertyRepository;
        public PropertyService(IPropertyRepository propertyRepository)
=======
namespace TPSS.Business.Service.Impl
{
    public sealed class PropertyService : IPropertyService
    {
        private readonly IPropertyRepository _propertyRepository;
        public PropertyService(IPropertyRepository propertyRepository) 
>>>>>>> DEV_THANG
        {
            _propertyRepository = propertyRepository;
        }

<<<<<<< HEAD
        private string AutoGenerateUserId()
        {
            string latestUserId = _userRepository.GetLatestUserIdAsync().Result;
            // giả sử định dạng user id của bạn là "USxxxxxxx"
            // trích xuất phần số và tăng giá trị lên 1, loại bỏ "US" lấy xxxxxxxx
            int numericpart = int.Parse(latestUserId.Substring(2));
            int newnumericpart = numericpart + 1;

            // tạo ra user id mới
            //us + "xxxxxxxx" | nếu số không đủ thì thay thế = 0 (d8)| 123 => 00000123
            string newuserid = $"US{newnumericpart:d8}";
            return newuserid;
        }


        public async Task<int> CreatePropertyAsync(PropertyDTO propertyDTO)
        {
            try
            {
                Property property = new Property();
                property.PropertyId = propertyDTO.PropertyId;
                property.ProjectId = propertyDTO.ProjectId;
                property.PropertyTitle = propertyDTO.PropertyTitle;
                property.Price = propertyDTO.Price;
                property.Image = propertyDTO.Image;
                property.Area = propertyDTO.Area;
                property.Province = propertyDTO.Province;
                property.City = propertyDTO.City;
                property.District = propertyDTO.District;
                property.Ward = propertyDTO.Ward;
                property.Street = propertyDTO.Street;

                int result = await _propertyRepository.CreatePropertyAsync(property);
                return result;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message, e);
            }
        }
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

        public async Task<int> UpdatePropertyAsync(PropertyDTO property)
        {
            //update soon
            throw new NotImplementedException();
        }

=======
        public async Task<dynamic> CreatePropertyAsync(PropertyDTO propertyDTO)
        {      
            try
            {
                Property property = new Property
                {
                    PropertyId = await AutoGeneratePropertyId(),
                    ProjectId = propertyDTO.ProjectId,
                    PropertyTitle = propertyDTO.PropertyTitle,
                    Price = propertyDTO.Price,
                    Image = propertyDTO.Image,
                    Area = propertyDTO.Area,
                    Province = propertyDTO.Province,
                    City = propertyDTO.City,
                    District = propertyDTO.District,
                    Ward = propertyDTO.Ward,
                    Street = propertyDTO.Street,
                    IsDelete = false
                };

                PropertyDetail propertyDetail = new PropertyDetail
                {
                    PropertyDetailId = await AutoGeneratePropertyDetailId(),
                    PropertyId = property.PropertyId,
                    OwnerId = propertyDTO.OwnerId,
                    PropertyTitle = propertyDTO.PropertyTitle,
                    Description = propertyDTO.Description,
                    CreateDate = DateTime.Now,
                    UpdateDate = null,
                    CreateBy = propertyDTO.CreateBy,
                    UpdateBy = null,
                    Images = propertyDTO.Images,
                    Service = propertyDTO.Service,
                    VerifyBy = null,
                    VerifyDate = null
                };
                int result = await _propertyRepository.CreatePropertyAsync(property, propertyDetail);

                return result;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<int> DeletePropertyAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<dynamic> GetListPropertyAsync(SearchPropertyDTO search)
        {
            throw new NotImplementedException();
        }

        public async Task<int> UpdatePropertyAsync(UserDTO user)
        {
            throw new NotImplementedException();
        }

        private async Task<string> AutoGeneratePropertyId()
        {
            string newpropertyid = "";
            string latestPropertyId = await _propertyRepository.GetLatestPropertyIdAsync();
            if (latestPropertyId.IsNullOrEmpty())
            {
                newpropertyid = "PP00000000";
            }
            else
            {
                // giả sử định dạng user id của bạn là "USxxxxxxx"
                // trích xuất phần số và tăng giá trị lên 1, loại bỏ "US" lấy xxxxxxxx
                int numericpart = int.Parse(latestPropertyId.Substring(2));
                int newnumericpart = numericpart + 1;

                // tạo ra user id mới
                //us + "xxxxxxxx" | nếu số không đủ thì thay thế = 0 (d8)| 123 => 00000123
                newpropertyid = $"PP{newnumericpart:d8}";
            }
            return newpropertyid;
        }

        private async Task<string> AutoGeneratePropertyDetailId()
        {
            string newpropertyid = "";
            string latestPropertyId = await _propertyRepository.GetLatestPropertyDetailIdAsync();
            if (latestPropertyId.IsNullOrEmpty())
            {
                newpropertyid = "PD00000000";
            }
            else
            {
                // giả sử định dạng user id của bạn là "USxxxxxxx"
                // trích xuất phần số và tăng giá trị lên 1, loại bỏ "US" lấy xxxxxxxx
                int numericpart = int.Parse(latestPropertyId.Substring(2));
                int newnumericpart = numericpart + 1;

                // tạo ra user id mới
                //us + "xxxxxxxx" | nếu số không đủ thì thay thế = 0 (d8)| 123 => 00000123
                newpropertyid = $"PD{newnumericpart:d8}";
            }
            return newpropertyid;
        }


        public async Task<PropertyDTO> GetPropertyByIDAsync(String id)
        {
            try
            {
                Property property = await _propertyRepository.GetPropertyByIDAsync(id);
                PropertyDetail propertyDetail = await _propertyRepository.GetPropertyDetailByIDAsync(id);
                PropertyDTO result = new PropertyDTO
                {
                    PropertyId = property.PropertyId,
                    ProjectId = property.ProjectId,
                    PropertyTitle = property.PropertyTitle,
                    Price = property.Price,
                    Image = property.Image,
                    Area = property.Area,
                    Province = property.Province,
                    City = property.City,
                    District = property.District,
                    Ward = property.Ward,
                    Street = property.Street,
                    PropertyDetailId = propertyDetail.PropertyDetailId,
                    OwnerId = propertyDetail.OwnerId,
                    Description = propertyDetail.Description,
                    CreateDate = propertyDetail.CreateDate,
                    UpdateDate = propertyDetail.UpdateDate,
                    CreateBy = propertyDetail.CreateBy,
                    UpdateBy = propertyDetail.UpdateBy,
                    Images = propertyDetail.Images,
                    Service = propertyDetail.Service,
                    VerifyBy = propertyDetail.VerifyBy,
                    VerifyDate = propertyDetail.VerifyDate
                };
                return result;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message, e);
            }
        }
>>>>>>> DEV_THANG
    }
}
