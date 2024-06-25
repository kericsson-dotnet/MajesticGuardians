using Microsoft.EntityFrameworkCore;

namespace Lexicon.Api.Repositories;

public class CrudRepository<T> : ICrudRepository<T> where T : class
{
    protected readonly DbContext _context;
    protected DbSet<T> DbSet;

    public CrudRepository(DbContext context)
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
}

