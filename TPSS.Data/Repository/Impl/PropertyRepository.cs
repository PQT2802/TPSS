using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPSS.Data.Helper;

namespace TPSS.Data.Repository.Impl
{
    public class PropertyRepository : BaseRepository, IPropertyRepository
    {
        public PropertyRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public Task<int> CreateContractAsync(Property CreateProperty)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteContractAsync(string PropertyId)
        {
            throw new NotImplementedException();
        }

        public Task<Property> GetContractByIdAsync(string PropertyId)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateContractAsync(Property updateProperty)
        {
            throw new NotImplementedException();
        }
    }
}
