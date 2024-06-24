using Lexicon.Api.Entities;
using Lexicon.Api.Models;
namespace Lexicon.Frontend.Services
{
    public interface IAuthService
    {
        Task<bool> LoginAsync(UserLoginModel model);
        Task LogoutAsync();

        User? GetCurrentUser();
        bool IsLoggedIn { get; }
        bool IsInRole(UserRole role);
    }
}
