namespace Lexicon.Frontend.Services
{
    public interface IUnitOfWork
    {
        IUserService UserService { get; }
        IAuthService AuthService { get; }
    }
}
