using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPSS.Data.Models.DTO
{
    public class CurrentUserObject
    {
        public string Email { get; set; }
        public string UserId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string RoleName { get; set; }
    }
}
