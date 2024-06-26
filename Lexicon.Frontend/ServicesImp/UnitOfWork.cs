using Lexicon.Frontend.Services;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.JSInterop;

namespace Lexicon.Frontend.ServicesImp
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly HttpClient _httpClient;
        private readonly LocalStorageService _localStorageService;

        public UnitOfWork(HttpClient httpClient, IJSRuntime jsRuntime)
        {
            _httpClient = httpClient;
            _localStorageService = new LocalStorageService(jsRuntime);
        }

        public IUserService UserService => new UserService(_httpClient);
        public IModuleService ModuleService => new ModuleService(_httpClient);
        public IActivityService ActivityService => new ActivityService(_httpClient);
        public ICourseService CourseService => new CourseService(_httpClient);
        public IDocumentService DocumentService => new DocumentService(_httpClient);
        public IAuthService AuthService => new AuthService(_httpClient);
        public IFileService FileService => new FileService(_httpClient);
    }
}
