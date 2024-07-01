using Lexicon.Frontend.Models;

namespace Lexicon.Frontend.Services;

public interface IModuleService
{
    Task<IEnumerable<Module>> GetModulesAsync();
    
    Task<Module> GetModuleAsync(int id);
    
    Task<bool> UpdateModuleAsync(int id, Module module);

    Task AddModuleAsync(Module module);

    Task DeleteModuleAsync(int id);
}
