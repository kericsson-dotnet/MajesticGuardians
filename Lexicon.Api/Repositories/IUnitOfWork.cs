using Lexicon.Api.Entities;

namespace Lexicon.Api.Repositories;

public interface IUnitOfWork
{
    ICrudRepository<Activity> Activities { get; }
    IDocumentRepository Documents { get; }
    ICourseRepository Courses { get; }
    IUserRepository Users { get; }
    IModuleRepository Modules { get; }
    Task SaveAsync();
}