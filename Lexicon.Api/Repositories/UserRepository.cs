using Lexicon.Api.Data;
using Lexicon.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lexicon.Api.Repositories;

public class UserRepository:IUserRepository

{
    private readonly LexiconLmsContext _dbContext;

    public UserRepository(LexiconLmsContext dbContext)
    {
            _dbContext = dbContext;
        }



    public async Task<User?> ValidateCredentialsAsync(string email, string password)
    {
    
            var normalizedEmail = email.ToLowerInvariant();
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == normalizedEmail);

      
            if (user == null || user.Password != password)
                return null;

            return user;
        }


}