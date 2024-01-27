using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPSS.Data.Models.DTO
{
    public class RegisterDTO
    {

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;
        public string ConfirmPassword { get; set; } = null!;

        public string Username { get; set; } = null!;

        public string Phone { get; set; } = null!;
    }
}
