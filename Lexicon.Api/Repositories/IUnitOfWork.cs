using Lexicon.Api.Entities;

namespace Lexicon.Api.Repositories;

public interface IUnitOfWork
{
    ICrudRepository<User> Users { get; }
    ICourseRepository Courses { get; }
    ICrudRepository<Module> Modules { get; }
    ICrudRepository<Activity> Activities { get; }
    IDocumentRepository Documents { get; }
    Task SaveAsync();
}
