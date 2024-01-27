using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPSS.Data.Models.DTO;
using TPSS.Data.Models.Entities;
using TPSS.Data.Repository;
using TPSS.Data.Repository.Impl;


namespace TPSS.Business.Service.Impl
{
    public class PropertyService : IPropertyService
    {
        private readonly IPropertyRepository _propertyRepository;
        public PropertyService(IPropertyRepository propertyRepository)
        {
            _propertyRepository = propertyRepository;
        }

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

    }
}
