using Lexicon.Frontend.Models;
using Lexicon.Frontend.Services;

namespace Lexicon.Frontend.ServicesImp;

public class ModuleService : IModuleService
{
    private readonly HttpClient _httpClient;

    public ModuleService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<Module>> GetModulesAsync() => await _httpClient.GetFromJsonAsync<IEnumerable<Module>>("api/modules");
    
    public async Task<Module> GetModuleAsync(int id) => await _httpClient.GetFromJsonAsync<Module>($"api/modules/{id}");
    
    public async Task<bool> UpdateModuleAsync(int id, Module module)
    {
        var res = await _httpClient.PutAsJsonAsync($"api/modules/{id}", module);
        return res.IsSuccessStatusCode;
    }

    public async Task AddModule(Module module) => await _httpClient.PostAsJsonAsync("api/modules", module);
}
