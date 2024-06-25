using Lexicon.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Lexicon.Api.Models;
using Lexicon.Api.Services;

namespace Lexicon.Api.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly TokenService _tokenService;

        public LoginController(IUserRepository userRepository,IConfiguration configuration)
        {
            _userRepository = userRepository;
            _tokenService = new TokenService(configuration);
        }

        [HttpPost("validate")]
        public async Task<ActionResult<bool>> ValidateUser([FromBody] UserLoginModel model)
        {
            var user = await _userRepository.ValidateCredentialsAsync(model.Email, model.Password);
            if (user == null)
            {           
                return Unauthorized();
            }
            string token;
            if (user.Role == Entities.UserRole.Teacher)
            {
                token = _tokenService.GenerateToken(model.Email, "Admin");
            }
            else
            {
                token = _tokenService.GenerateToken(model.Email, "User");
            }
       
            return Ok(new { Token = token });

        }

    }
}
