using Lexicon.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Lexicon.Api.Models;
using Lexicon.Api.Services;
using Lexicon.Api.Dtos.UserDtos;
using AutoMapper;

namespace Lexicon.Api.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly TokenService _tokenService;
        private readonly IMapper _mapper;

        public LoginController(IUserRepository userRepository, IConfiguration configuration, IMapper mapper)
        {
            _userRepository = userRepository;
            _tokenService = new TokenService(configuration);
            _mapper = mapper;
        }

        [HttpPost("validate")]
        public async Task<ActionResult> ValidateUser([FromBody] UserDto userDto)
        {
            var user = await _userRepository.ValidateCredentialsAsync(userDto.Email, userDto.Password);
            
            if (user == null)
            {
                return Unauthorized();
            }

            string token = _tokenService.GenerateToken(_mapper.Map<UserDto>(user));
            
            return Ok(token);
        }

    }
}
