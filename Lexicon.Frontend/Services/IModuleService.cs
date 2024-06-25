using Lexicon.Frontend.Models;

namespace Lexicon.Frontend.Services;

public interface IModuleService
{
    Task<IEnumerable<Module>> GetModulesAsync();
}

