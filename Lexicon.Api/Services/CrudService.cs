using Lexicon.Api.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Lexicon.Api.Services;

public class CrudService<T> : ICrudRepository<T> where T : class
{
    protected readonly DbContext _context;
    protected DbSet<T> DbSet;

    public CrudService(DbContext context)
    {
        _context = context;
        DbSet = _context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await DbSet.ToListAsync();
    }

    public async Task<T> GetAsync(object id)
    {
        return await DbSet.FindAsync(id) ?? throw new InvalidOperationException($"{typeof(T).Name} Id {id} not found.");
    }

    public void Add(T obj)
    {
        DbSet.Add(obj);
    }

    public void Update(T obj)
    {
        DbSet.Attach(obj);
        _context.Entry(obj).State = EntityState.Modified;
    }

    public void Delete(object id)
    {
        T existing = DbSet.Find(id) ?? throw new InvalidOperationException($"{typeof(T).Name}Id {id} not found.");
        DbSet.Remove(existing);
    }

    public async Task<int> CountAsync()
    {
        return await DbSet.CountAsync();
    }
}

