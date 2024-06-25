using Lexicon.Api.Models;
using Lexicon.Frontend.Services;
using Microsoft.AspNetCore.Mvc;
using static Lexicon.Frontend.Components.Pages.Login;
using static System.Net.WebRequestMethods;

namespace Lexicon.Frontend.ServicesImp
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private bool isAuthenticated = false; 

        public AuthService(HttpClient httpClient)
        {
           _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> LoginAsync(UserLoginModel model)
        {
            return await _httpClient.PostAsJsonAsync("api/login/validate", model);
        }
        public Task LogoutAsync()
        {
            isAuthenticated = false;
    
            return Task.CompletedTask;
        }
    }
}
