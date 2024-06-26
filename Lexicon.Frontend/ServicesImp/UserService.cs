using Lexicon.Frontend.Models;
using Lexicon.Frontend.Services;
using Microsoft.AspNetCore.Mvc;
namespace Lexicon.Frontend.ServicesImp
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<User>> GetUsersAsync() => await _httpClient.GetFromJsonAsync<IEnumerable<User>>("api/users");

        public async Task<IEnumerable<Activity>> GetActivityAsync() => await _httpClient.GetFromJsonAsync<IEnumerable<Activity>>("api/activity");

        public async Task CreateUserAsync(User user) => await _httpClient.PostAsJsonAsync("api/users", user);
    }
}
