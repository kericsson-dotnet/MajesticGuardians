using Lexicon.Frontend.ServicesImp;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Lexicon.Frontend.Services
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient _httpClient;
        private readonly ISessionStorageService _services;
        private bool _isInitialized;

        public CustomAuthenticationStateProvider(HttpClient httpClient, ISessionStorageService services)
        {
            _httpClient = httpClient;
            _services = services;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            if (!_isInitialized)
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            var token = await _services.GetItemAsync("authToken");
            var identity = string.IsNullOrWhiteSpace(token)
                ? new ClaimsIdentity()
                : new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token), "jwt");

            _httpClient.DefaultRequestHeaders.Authorization =
                string.IsNullOrWhiteSpace(token)
                    ? null
                    : new AuthenticationHeaderValue("Bearer", token);

            return new AuthenticationState(new ClaimsPrincipal(identity));
        }

        public async Task MarkUserAsAuthenticated(string token)
        {
            await _services.SetItemAsync("authToken", token);
            var identity = new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token), "jwt");
            var user = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        public async Task MarkUserAsLoggedOut()
        {
            await _services.RemoveItemAsync("authToken");
            var identity = new ClaimsIdentity();
            var user = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        public void SetInitialized()
        {
            _isInitialized = true;
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
