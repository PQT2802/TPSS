﻿using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPSS.Data.Helper

{
    public abstract class BaseRepository
    {
        private readonly IConfiguration _configuration;
        protected BaseRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected IDbConnection CreateConnection()
        {
<<<<<<< HEAD
            return new SqlConnection(_configuration.GetConnectionString("DEV_TRONG"));
=======
            return new SqlConnection(_configuration.GetConnectionString("DEV_HUNG"));
>>>>>>> 3e0e4aad604d3b787cf464e314033106a70d10b4
            
        }
    }
}