using Lexicon.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lexicon.Api.Repositories;

public class UserRepository(DbContext context) : CrudRepository<User>(context), IUserRepository

{
    public virtual async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Set<User>()
            .Include(u => u.Courses)
            .Include(u => u.Documents)
            .ToListAsync();
    }

    public virtual async Task<User> GetAsync(object id)
    {
        if (id is int userId)
        {
            return await _context.Set<User>()
                       .Include(u => u.Courses)
                       .Include(u => u.Documents)
                       .FirstOrDefaultAsync(u => u.UserId == userId) ??
                   throw new InvalidOperationException($"{typeof(User).Name} Id {id} not found.");
        }
        throw new ArgumentException("Invalid ID type");
    }

    public async Task<User?> ValidateCredentialsAsync(string email, string password)
    {
        var normalizedEmail = email.ToLowerInvariant();
        var user = await _context.Set<User>().FirstOrDefaultAsync(u => u.Email.ToLower() == normalizedEmail);

        if (user == null || user.Password != password)
            return null;

        return user;
    }
}