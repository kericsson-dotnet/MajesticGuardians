using Lexicon.Api.Models;
using Lexicon.Frontend.Services;
using Microsoft.JSInterop;
using System;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Lexicon.Frontend.ServicesImp
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly SessionStorageService _localStorageService;
        private string? _token;
        private bool _isAuthenticated; 

        public AuthService(HttpClient httpClient, SessionStorageService localStorageService)
        {
            _httpClient = httpClient;
            _localStorageService = localStorageService;
            _token = string.Empty;
            _isAuthenticated = false;
        }
        public bool IsAuthenticated() => _isAuthenticated && !string.IsNullOrEmpty(_token);

        public async Task<string?> LoginAsync(UserLoginModel model)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/login/validate", model);

                if (response.IsSuccessStatusCode)
                {
                    var token = await response.Content.ReadAsStringAsync();
                    _token = token;

                    await _localStorageService.SetItemAsync("authToken", token);
                    _isAuthenticated = true;
                    return token;
                }

                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Inloggning misslyckades", ex);
            }
        }

        public async Task LogoutAsync()
        {
            _token = null;
            _isAuthenticated = false;
            await _localStorageService.RemoveItemAsync("authToken");
        }

    }
}
