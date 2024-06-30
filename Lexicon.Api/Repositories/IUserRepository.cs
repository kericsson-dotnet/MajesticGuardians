using Lexicon.Api.Entities;

namespace Lexicon.Api.Repositories;

public interface IUserRepository : ICrudRepository<User>
{
    Task<User?> ValidateCredentialsAsync(string email, string password);
}