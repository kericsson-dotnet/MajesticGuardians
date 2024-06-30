using Microsoft.EntityFrameworkCore;
using Lexicon.Api.Entities;
using Lexicon.Api.Data;

namespace Lexicon.Api.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly LexiconLmsContext _context;

    public ICrudRepository<Activity> Activities { get; private set; }

    public UnitOfWork(LexiconLmsContext context)
    {
        _context = context;
        Activities = new CrudRepository<Activity>(_context);
    }

    public IDocumentRepository Documents => new DocumentRepository(_context);

    public ICourseRepository Courses => new CourseRepository(_context);
    public IUserRepository Users => new UserRepository(_context);
    public IModuleRepository Modules => new ModuleRepository(_context);
    

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}