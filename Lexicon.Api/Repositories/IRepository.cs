namespace Lexicon.Api.Repositories;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetAsync(object id);
    void Add(T obj);
    void Update(T obj);
    void Delete(object id);
}

