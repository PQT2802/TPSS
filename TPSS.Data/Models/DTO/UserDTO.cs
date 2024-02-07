using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPSS.Data.Models.DTO
{
    public class UserDTO
    {
        public string UserId { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Username { get; set; } = null!;

        public string Phone { get; set; } = null!;

<<<<<<< HEAD
        public bool? IsDelete { get; set; } //mac dich la false hoi thang
=======
        public bool? IsDelete { get; set; }

        public string RoleId { get; set; } = null!;

        public bool? IsActive { get; set; }
>>>>>>> DEV_THANG
    }
}
