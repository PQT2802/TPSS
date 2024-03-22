using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPSS.Data.Models.Entities;

namespace TPSS.Data.Repository
{
    public interface  IAddressRepository
    {
        public Task<IEnumerable<Address>> GetCity();
        public Task<IEnumerable<string>> GetDistrict(string addressId);
        public Task<IEnumerable<string>> GetWard(string addressId, string district);
    }
}
