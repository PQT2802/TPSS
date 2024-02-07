using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPSS.Data.Models.DTO
{
    public class LoginDTO
    {
        public string Password { get; set; } = null!;
        public string Username { get; set; } = null!;
        //public string UsernameOrEmailOrPhone { get; set; } = null!;
        
    }
}
