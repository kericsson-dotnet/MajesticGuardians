using Lexicon.Api.Entities;

namespace Lexicon.Api.Repositories;

public interface IUnitOfWork
{
    IDocumentRepository Documents { get; }
    ICourseRepository Courses { get; }
    IUserRepository Users { get; }
    IModuleRepository Modules { get; }
    IActivityRepository Activities { get; }
    Task SaveAsync();
}