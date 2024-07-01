using Lexicon.Api.Entities;
using Lexicon.Api.Data;

namespace Lexicon.Api.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly LexiconLmsContext _context;

    public UnitOfWork(LexiconLmsContext context)
    {
        _context = context;
    }

    public IUserRepository Users => new UserRepository(_context);
    public ICourseRepository Courses => new CourseRepository(_context);
    public IModuleRepository Modules => new ModuleRepository(_context);
    public IActivityRepository Activities => new ActivityRepository(_context);

    public IDocumentRepository Documents => new DocumentRepository(_context);
    

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}