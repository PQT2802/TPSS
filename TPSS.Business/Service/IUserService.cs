
using TPSS.Data.Models.DTO;
using TPSS.Data.Models.Entities;

namespace TPSS.Business.Service
{
    public interface IUserService
    {
        public Task<dynamic> RegistUserAsync(RegisterDTO registerDTO, string confirmCode);
        public  Task<User> GetUserByIdAsync(String id);
        public  Task<dynamic> CreateUserAsync(UserDTO user);
        public Task<dynamic> UpdateUserAsync(UpdateUserObject updateUser);
        public Task<int> DeleteUserAsync(String id);
        public Task<dynamic> GetUserAccountAsync(LoginDTO loginDTO);
        public  Task<dynamic> GetInforUserAsync(string userId);
        public Task<int> UpdateUserRole(string userId, string roleId);
        public Task<int> UpdateIsActive(string userId);
        public Task<ResponseObject> GetTokenFirebase(string firebaseToken);
        public Task SendConfirmationEmail(string toEmailAddress);
    }
}
