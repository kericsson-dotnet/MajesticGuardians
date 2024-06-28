using Lexicon.Frontend.Models;
using Microsoft.AspNetCore.Mvc;
namespace Lexicon.Frontend.Services
{
    public interface IUserService
    { 
        Task<IEnumerable<User>> GetUsersAsync();
		
        Task<User> GetUserAsync(int id);

		Task CreateUserAsync(User user);

        Task<User?> GetCurrentUserAsync();
    }
}
