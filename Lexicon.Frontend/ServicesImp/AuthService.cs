using Lexicon.Api.Entities;
using Lexicon.Api.Models;
using Lexicon.Frontend.Services;
using System.Net.Http.Json;

namespace Lexicon.Frontend.ServicesImp
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private bool _isAuthenticated = false;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public bool IsAuthenticated() => _isAuthenticated;

        public async Task<HttpResponseMessage> LoginAsync(UserLoginModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("api/login/validate", model);
            if (response.IsSuccessStatusCode)
            {
                _isAuthenticated = true;
            }
            return response;
        }

        public Task LogoutAsync()
        {
            _isAuthenticated = false;
            return Task.CompletedTask;
        }

    }
}
