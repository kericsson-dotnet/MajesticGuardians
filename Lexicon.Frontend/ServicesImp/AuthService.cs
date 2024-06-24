using Lexicon.Api.Entities;
using Lexicon.Api.Models;
using Lexicon.Frontend.Services;
using System.Net.Http.Json;

namespace Lexicon.Frontend.ServicesImp
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private User? currentUser;
        private bool isAuthenticated = false;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> LoginAsync(UserLoginModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("api/login/validate", model);

            if (response.IsSuccessStatusCode)
            {
                // Anta att API:et returnerar en User-objekt vid lyckad inloggning
                currentUser = await response.Content.ReadFromJsonAsync<User>();
                isAuthenticated = true;
                return true;
            }
            else
            {
                return false;
            }
        }

        public Task LogoutAsync()
        {
            currentUser = null;
            isAuthenticated = false;
            return Task.CompletedTask;
        }

        public User? GetCurrentUser()
        {
            return currentUser;
        }

        public bool IsLoggedIn => isAuthenticated;

        public bool IsInRole(UserRole role)
        {
            return currentUser?.Role == role;
        }
    }
}
