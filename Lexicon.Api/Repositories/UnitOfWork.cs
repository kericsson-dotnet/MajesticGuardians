using Microsoft.EntityFrameworkCore;
using Lexicon.Api.Entities;
using Lexicon.Api.Data;

namespace Lexicon.Api.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly LexiconLmsContext _context;

    public ICrudRepository<User> Users { get; private set; }
    public ICrudRepository<Module> Modules { get; private set; }
    public ICrudRepository<Activity> Activities { get; private set; }

    public UnitOfWork(LexiconLmsContext context)
    {
        _context = context;
        Users = new CrudRepository<User>(_context);
        Modules = new CrudRepository<Module>(_context);
        Activities = new CrudRepository<Activity>(_context);
    }

    public IDocumentRepository Documents => new DocumentRepository(_context);

    public ICourseRepository Courses => new CourseRepository(_context);

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

}

