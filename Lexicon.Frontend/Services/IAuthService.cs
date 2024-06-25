using Lexicon.Api.Entities;
using Lexicon.Api.Models;
namespace Lexicon.Frontend.Services
{
    public interface IAuthService
    {
        Task<HttpResponseMessage> LoginAsync(UserLoginModel model);
        Task LogoutAsync();
    }
}
