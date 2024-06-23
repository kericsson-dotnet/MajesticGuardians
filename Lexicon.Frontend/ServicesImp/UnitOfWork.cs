using Lexicon.Frontend.Services;

namespace Lexicon.Frontend.ServicesImp
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HttpClient _httpClient;

        public UnitOfWork(HttpClient httpClient)
        {
            _httpClient  = httpClient;
        }

        public IUserService UserService => new UserService(_httpClient);
        public IAuthService AuthService => new AuthService(_httpClient);
    }
}
