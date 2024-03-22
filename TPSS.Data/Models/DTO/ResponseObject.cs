using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPSS.Data.Models.DTO
{
    public class ResponseObject
    {
        public string UserId { get; set; }
        public string Avatar { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }
}
