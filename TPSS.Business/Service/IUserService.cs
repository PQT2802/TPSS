using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPSS.Data.Models.DTO;
using TPSS.Data.Models.Entities;

namespace TPSS.Business.Service
{
    public interface IUserService
    {
        public Task<dynamic> RegistUserAsync(RegisterDTO registerDTO);
        public  Task<User> GetUserByIdAsync(String id);
        public  Task<dynamic> CreateUserAsync(UserDTO user);
        public Task<int> UpdateUserAsync(UserDTO user);
        public Task<int> DeleteUserAsync(String id);
        public Task<dynamic> GetUserAccountAsync(LoginDTO loginDTO);


    }
}
