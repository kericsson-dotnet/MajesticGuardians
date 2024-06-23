using Lexicon.Api.Models;
using Lexicon.Frontend.Services;

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

        public async Task<bool> LoginAsync(UserLoginModel model)
        {


            var response = await _httpClient.PostAsJsonAsync("api/login/validate", model);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<bool>();
            }
            else
            {
               
                return false;
            }
        }

        public Task LogoutAsync()
        {
            isAuthenticated = false;
    
            return Task.CompletedTask;
        }
    }
}
