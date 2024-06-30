using Lexicon.Api.Entities;

namespace Lexicon.Api.Repositories;

public interface IUnitOfWork
{
    ICrudRepository<Module> Modules { get; }
    ICrudRepository<Activity> Activities { get; }
    IDocumentRepository Documents { get; }
    ICourseRepository Courses { get; }
    IUserRepository Users { get; }
    Task SaveAsync();
}