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
        public IModuleService ModuleService => new ModuleService(_httpClient);
        public IActivityService ActivityService => new ActivityService(_httpClient);
        public ICourseService CourseService => new CourseService(_httpClient);
        public IDocumentService DocumentService => new DocumentService(_httpClient);
        public IAuthService AuthService => new AuthService(_httpClient);
    }
}
