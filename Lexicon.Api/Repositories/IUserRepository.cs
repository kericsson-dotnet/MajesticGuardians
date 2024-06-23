using Lexicon.Api.Entities;

namespace Lexicon.Api.Repositories
{
    public interface IUserRepository
    {
    
        Task<User?> ValidateCredentialsAsync(string email, string password);
    }
}