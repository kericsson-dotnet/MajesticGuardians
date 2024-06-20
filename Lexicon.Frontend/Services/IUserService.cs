using Lexicon.Frontend.Models;

namespace Lexicon.Frontend.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsersAsync();
    }
}
