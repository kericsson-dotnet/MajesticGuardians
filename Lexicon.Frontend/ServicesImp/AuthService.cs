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
        private readonly LocalStorageService _localStorageService;
        private bool _isAuthenticated = false;
        private string _token;

        public AuthService(HttpClient httpClient, LocalStorageService localStorageService)
        {
            _httpClient = httpClient;
            _localStorageService = localStorageService;
        }

        public async Task<string> LoginAsync(UserLoginModel model)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/login/validate", model);

                if (response.IsSuccessStatusCode)
                {
                    var token = await response.Content.ReadAsStringAsync();
                    _token = token;
                    _isAuthenticated = true;

                    // Spara token i localStorage
                    await _localStorageService.SetItemAsync("authToken", token);

                    return token;
                }
                else
                {
                    _isAuthenticated = false;
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
            _isAuthenticated = false;
            _token = null;

            // Ta bort token från localStorage
            await _localStorageService.RemoveItemAsync("authToken");
        }

        public async Task<string> GetTokenAsync()
        {
            if (!string.IsNullOrEmpty(_token))
            {
                return _token;
            }

            // Hämta token från localStorage om det finns
            _token = await _localStorageService.GetItemAsync("authToken");
            return _token;
        }

        public bool IsAuthenticated()
        {
            // Kontrollera om användaren är autentiserad genom att kolla om _token är satt och giltigt
            return _isAuthenticated && !string.IsNullOrEmpty(_token);
        }
    }
}
