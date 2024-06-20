using Microsoft.AspNetCore.Cors.Infrastructure;

namespace Lexicon.Frontend.Services
{
    public interface IUnitOfWork
    {
        IUserService UserService { get; }
        ICourseService CourseService { get; }
    }
}
