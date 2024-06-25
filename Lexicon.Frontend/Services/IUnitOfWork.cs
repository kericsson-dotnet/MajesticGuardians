using Microsoft.AspNetCore.Cors.Infrastructure;

namespace Lexicon.Frontend.Services;

public interface IUnitOfWork
{
    IUserService UserService { get; }
    IModuleService ModuleService { get; }
    IAuthService AuthService { get; }
    ICourseService CourseService { get; }
}

