using Lexicon.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Lexicon.Api.Models;

namespace Lexicon.Api.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly IUserRepository _userRepository;

        public LoginController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("validate")]
        public async Task<ActionResult<bool>> ValidateUser([FromBody] UserLoginModel model)
        {
            var user = await _userRepository.ValidateCredentialsAsync(model.Email, model.Password);
            if (user == null)
            {
                Console.WriteLine("Cannot find the user");
                return Ok(false); 
            }
            else
            {
                Console.WriteLine(" find the user");
                return Ok(true); 
            }

        }

    }
}
