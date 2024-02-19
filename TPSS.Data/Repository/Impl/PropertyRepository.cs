
﻿using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using TPSS.Data.Helper;
using TPSS.Data.Models.Entities;



using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPSS.Data.Helper;
using TPSS.Data.Models.Entities;

namespace TPSS.Data.Repository.Impl
{
    public class PropertyRepository : BaseRepository, IPropertyRepository
    {
        public PropertyRepository(IConfiguration configuration) : base(configuration)
        {
        }

        Task<int> IPropertyRepository.CreatePropertyAsync(Property property)
        {
            throw new NotImplementedException();
        }

        Task<int> IPropertyRepository.DeletePropertyAsync(string id)
        {
            throw new NotImplementedException();
        }

        Task<Property> IPropertyRepository.GetPropertyByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Property>> GetPropertyForHomePage()
        {
            try
            {
                var query = "SELECT * FROM Property LIMIT 100";
                using var connection = CreateConnection();
                return await connection.QueryAsync<Property>(query);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        Task<int> IPropertyRepository.UpdatePropertyAsync(Property property)
        {
            throw new NotImplementedException();
        }
    }
