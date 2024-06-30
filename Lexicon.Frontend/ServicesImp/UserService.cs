using Lexicon.Frontend.Models;
using Lexicon.Frontend.Services;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
namespace Lexicon.Frontend.ServicesImp;

public class UserService : IUserService
{
    private readonly HttpClient _httpClient;
    private readonly ISessionStorageService _sessionStorageService;
    private readonly CustomAuthenticationStateProvider _customAuthStateProvider;
    public UserService(HttpClient httpClient, ISessionStorageService sessionStorageService)
    {
            _httpClient = httpClient;
            _sessionStorageService = sessionStorageService;
            _customAuthStateProvider = new CustomAuthenticationStateProvider(_httpClient, _sessionStorageService);
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

    public async Task<User> GetUserAsync(int id)
    {
            //await AddTokenToRequestHeader(); Tog bort Authorize från API förtillfället
            return await _httpClient.GetFromJsonAsync<User>($"api/users/{id}");
        }

    public async Task<User?> GetCurrentUserAsync()
    {
            User userData = new User();
            ClaimsPrincipal user;
           
            _customAuthStateProvider.SetInitialized();
            var authState = await _customAuthStateProvider.GetAuthenticationStateAsync();

            user = authState.User;

            if (!user.Identity.IsAuthenticated)
            {
                return null;
            }

            var userIdClaim = user.FindFirst("userId")?.Value;

            if (int.TryParse(userIdClaim, out int userId))
            {
                userData = await GetUserAsync(userId);
            }

            return userData;
        }

    public async Task UpdateUserAsync(User user)
    {
            var response = await _httpClient.PutAsJsonAsync($"api/users/{user.UserId}", user);
            response.EnsureSuccessStatusCode();
        }

    public async Task DeleteUserAsync(int id) => await _httpClient.DeleteAsync($"api/users/{id}");

}