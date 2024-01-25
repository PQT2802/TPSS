using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPSS.Data.Repository
{
    public interface IPropertyRepository
    {
        public Task<Property> GetContractByIdAsync(string PropertyId);
        public Task<int> CreateContractAsync(Property CreateProperty);
        public Task<int> UpdateContractAsync(Property updateProperty);
        public Task<int> DeleteContractAsync(string PropertyId);
    }
}
