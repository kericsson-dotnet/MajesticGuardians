using Lexicon.Api.Entities;
using Lexicon.Api.Models;
namespace Lexicon.Frontend.Services
{
    public interface IAuthService
    { 
        Task<string> LoginAsync(UserLoginModel model);
        public bool IsAuthenticated();
        Task LogoutAsync();
    }
}
