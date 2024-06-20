using Lexicon.Api.Entities;

namespace Lexicon.Api.Repositories;

public interface IUnitOfWork
{
    IRepository<User> Users { get; }
    IRepository<Course> Courses { get; }
    IRepository<Module> Modules { get; }
    IRepository<Activity> Activities { get; }
    IRepository<Document> Documents { get; }
    Task SaveAsync();
}
