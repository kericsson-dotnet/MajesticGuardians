using Lexicon.Api.Dtos.UserDtos;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Lexicon.Api.Services
{
    public class TokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(UserDto userDto)
        {

            var claims = new[]
            {
                new Claim("userId", userDto.UserId.ToString()),
                new Claim("email",userDto.Email ),
                new Claim("role", userDto.Role.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var creds = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(
               _configuration["Jwt:Issuer"],
               _configuration["Jwt:Audience"],
               claims,
               expires: DateTime.Now.AddMinutes(30),
               signingCredentials: creds);

            // var tokenHandler = new JwtSecurityTokenHandler();
            // var token = tokenHandler.CreateToken(tokenDescriptor);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
