using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TPSS.Data.Models.DTO;

namespace TPSS.Business.Common
{
    public class TokenHepler
    {
        private static TokenHepler instance;
        public static TokenHepler Instance
        {
            get { if (instance == null) instance = new TokenHepler(); return Common.TokenHepler.instance; }
            private set { Common.TokenHepler.instance = value; }
        }
        public string CreateToken(List<Claim> authClaims, IConfiguration _configuration)
        {

            var authenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.UtcNow.AddDays(60),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authenKey, SecurityAlgorithms.HmacSha256)
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
        public  async Task<CurrentUserObject> GetThisUserInfo(HttpContext httpContext)
        {
            CurrentUserObject currentUser = new();

            var checkUser = httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
            if (checkUser != null)
            {
                currentUser.UserId = httpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId").Value;
                currentUser.RoleName = httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
                currentUser.Email = httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
            }
            else
            {
                return null;
            }
            return currentUser;
        }
    }
}
