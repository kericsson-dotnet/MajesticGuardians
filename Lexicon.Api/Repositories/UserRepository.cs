using Lexicon.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lexicon.Api.Repositories;

public class UserRepository(DbContext context) : CrudRepository<User>(context), IUserRepository

{
    public async Task<User?> ValidateCredentialsAsync(string email, string password)
    {
        var normalizedEmail = email.ToLowerInvariant();
        var user = await _context.Set<User>().FirstOrDefaultAsync(u => u.Email.ToLower() == normalizedEmail);

        if (user == null || user.Password != password)
            return null;

        return user;
    }
}