using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

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
//<<<<<<< HEAD
            return new SqlConnection(_configuration.GetConnectionString("DEV_NGOO"));
//=======
            return new SqlConnection(_configuration.GetConnectionString("DEV_HUNG"));
//>>>>>>> master
            
        }
    }
}