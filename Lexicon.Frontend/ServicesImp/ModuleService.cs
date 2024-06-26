﻿using Lexicon.Frontend.Models;
using Lexicon.Frontend.Services;
using System.Net.Http;
using System.Net;

namespace Lexicon.Frontend.ServicesImp;

public class ModuleService : IModuleService
{
    private readonly HttpClient _httpClient;

    public ModuleService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<Module>> GetModulesAsync() => await _httpClient.GetFromJsonAsync<IEnumerable<Module>>("api/modules");
    
    public async Task<Module> GetModuleAsync(int id)
    {
		try
		{
			return await _httpClient.GetFromJsonAsync<Module>($"api/modules/{id}");
		}

		catch (HttpRequestException ex) when(ex.StatusCode == HttpStatusCode.NotFound)
        {
	        return null;
        }
	}
    
    public async Task<bool> UpdateModuleAsync(int id, Module module)
    {
        var res = await _httpClient.PutAsJsonAsync($"api/modules/{id}", module);
        return res.IsSuccessStatusCode;
    }

    public async Task AddModuleAsync(Module module) => await _httpClient.PostAsJsonAsync("api/modules", module);

    public async Task DeleteModuleAsync(int id) => await _httpClient.DeleteAsync($"api/modules/{id}");
}
