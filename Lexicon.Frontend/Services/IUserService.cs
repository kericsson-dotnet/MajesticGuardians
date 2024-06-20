using Lexicon.Frontend.Models;
using Microsoft.AspNetCore.Mvc;
namespace Lexicon.Frontend.Services
{
    public interface IUserService
    { 
        Task<IEnumerable<User>> GetUsersAsync();

        Task<IEnumerable<Activities>> GetActivityAsync();
    }
}
