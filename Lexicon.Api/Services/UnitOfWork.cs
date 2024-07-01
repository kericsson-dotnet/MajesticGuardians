using Lexicon.Api.Entities;
using Lexicon.Api.Data;
using Lexicon.Api.Repositories;

namespace Lexicon.Api.Services;

public class UnitOfWork : IUnitOfWork
{
    private readonly LexiconLmsContext _context;

    public UnitOfWork(LexiconLmsContext context)
    {
        _context = context;
    }

    public IUserRepository Users => new UserRepository(_context);
    public ICourseRepository Courses => new CourseService(_context);
    public IModuleRepository Modules => new ModuleService(_context);
    public IActivityRepository Activities => new ActivityService(_context);

    public IDocumentRepository Documents => new DocumentService(_context);


    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}