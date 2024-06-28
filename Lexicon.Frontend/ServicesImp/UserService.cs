using Lexicon.Frontend.Models;
using Lexicon.Frontend.Services;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
namespace Lexicon.Frontend.ServicesImp
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        private readonly ISessionStorageService _sessionStorageService;

        public UserService(HttpClient httpClient, ISessionStorageService sessionStorageService)
        {
            _httpClient = httpClient;
            _sessionStorageService = sessionStorageService;
        }

        private async Task AddTokenToRequestHeader()
        {
            var token = await _sessionStorageService.GetItemAsync("authToken");

            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            await AddTokenToRequestHeader();
            return await _httpClient.GetFromJsonAsync<IEnumerable<User>>("api/users");
        }

        public async Task CreateUserAsync(User user) => await _httpClient.PostAsJsonAsync("api/users", user);

        public async Task<User?> GetCurrentUserAsync()
        {
            var token = await _sessionStorageService.GetItemAsync("authToken");

            if (string.IsNullOrEmpty(token))
            {
                Console.WriteLine("Token is null or empty");
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);

            Console.WriteLine("JWT Token: " + jwtToken);

            var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
            var roleClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "role")?.Value;
            var emailClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "email")?.Value;

            Console.WriteLine("UserId Claim: " + userIdClaim);
            Console.WriteLine("Role Claim: " + roleClaim);
            Console.WriteLine("Email Claim: " + emailClaim);

            if (!int.TryParse(userIdClaim, out var userId) || string.IsNullOrEmpty(roleClaim) || emailClaim == null)
            {
                return null;
            }

            if (!Enum.TryParse(roleClaim, out UserRole role))
            {
                return null;
            }

            var user = new User
            {
                UserId = userId,
                Role = role,
                Email = emailClaim,
            };

            return user;
        }

		public async Task<User> GetUserAsync(int id) => await _httpClient.GetFromJsonAsync<User>($"api/users/{id}");
	}
}
