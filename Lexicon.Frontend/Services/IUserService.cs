using Lexicon.Frontend.Models;

namespace Lexicon.Frontend.Services;

public interface IUserService
{
    Task<IEnumerable<User>> GetUsersAsync();
    Task<User> GetUserAsync(int id);
    Task CreateUserAsync(User user);
    Task UpdateUserAsync(User user);
}
