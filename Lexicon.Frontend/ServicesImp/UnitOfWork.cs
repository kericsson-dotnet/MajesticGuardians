using Lexicon.Frontend.Services;
using Microsoft.AspNetCore.Cors.Infrastructure;

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
        public ICourseService CourseService => new CourseService(_httpClient);
    }
}
