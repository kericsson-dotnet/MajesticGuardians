using Microsoft.EntityFrameworkCore;
using Lexicon.Api.Entities;
using Lexicon.Api.Data;

namespace Lexicon.Api.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly LexiconLmsContext _context;

    public IRepository<User> Users { get; private set; }
    public IRepository<Course> Courses { get; private set; }
    public IRepository<Module> Modules { get; private set; }
    public IRepository<Activity> Activities { get; private set; }
    public IRepository<Document> Documents { get; private set; }

    public UnitOfWork(LexiconLmsContext context)
    {
        _context = context;
        Users = new Repository<User>(_context);
        Courses = new Repository<Course>(_context);
        Modules = new Repository<Module>(_context);
        Activities = new Repository<Activity>(_context);
        Documents = new Repository<Document>(_context);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

}

