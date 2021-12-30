using EcommShop.DataAccessor.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ECommShop.Business.Helpers
{
    public class TokenHelpers
    {
        private readonly IConfiguration _config;
        public TokenHelpers(IConfiguration config)
        {
            _config = config;
        }
        public string GenerateJSONWebToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim("Id", user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email.ToString()),
                new Claim(ClaimTypes.Role, user.UserType.ToString()),
                new Claim("FirstName" , user.FirstName.ToString()),
                new Claim("LastName" , user.LastName.ToString()),
                new Claim("PhoneNumber" , user.PhoneNumber.ToString()),
                new Claim("Addresss" , user.Addresss.ToString()),
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
