using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPSS.Data.Models.Entities;
using TPSS.Data.Repository;
using TPSS.Data.Repository.Impl;

namespace TPSS.Business.Service.Impl
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepsitory;
        public AddressService (IAddressRepository addressRepsitory)
        {
            _addressRepsitory = addressRepsitory;
        }

        public async Task<IEnumerable<Address>> GetCity()
        {
            try
            {
                var result = await _addressRepsitory.GetCity();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<IEnumerable<string>> GetDistrict(string addressId)
        {
            try
            {
                var result = await _addressRepsitory.GetDistrict(addressId);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<IEnumerable<string>> GetWard(string addressId, string district)
        {
            try
            {
                var result = await _addressRepsitory.GetWard(addressId, district);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
