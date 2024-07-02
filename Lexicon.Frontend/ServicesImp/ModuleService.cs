using Lexicon.Frontend.Models;
using Lexicon.Frontend.Services;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;

namespace Lexicon.Frontend.ServicesImp;

public class ModuleService : IModuleService
{
    private readonly HttpClient _httpClient;
	private readonly ISessionStorageService _sessionStorageService;


	public ModuleService(HttpClient httpClient, ISessionStorageService sessionStorageService)
	{
		_httpClient = httpClient;
		_sessionStorageService = sessionStorageService;
	}

	private async Task AddTokenToRequestHeader()
	{
		var token = await _sessionStorageService.GetItemAsync("authToken");

		if (!string.IsNullOrEmpty(token))
		{
			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
		}
	}

	public async Task<IEnumerable<Module>> GetModulesAsync()
	{
		await AddTokenToRequestHeader();
		return await _httpClient.GetFromJsonAsync<IEnumerable<Module>>("api/modules");
	}

    public async Task<Module> GetModuleAsync(int id)
    {
		try
		{
			await AddTokenToRequestHeader();
			return await _httpClient.GetFromJsonAsync<Module>($"api/modules/{id}");
		}

		catch (HttpRequestException ex) when(ex.StatusCode == HttpStatusCode.NotFound)
        {
	        return null;
        }
	}
    
    public async Task<bool> UpdateModuleAsync(int id, Module module)
    {
		await AddTokenToRequestHeader();

		var res = await _httpClient.PutAsJsonAsync($"api/modules/{id}", module);
        return res.IsSuccessStatusCode;
    }

	public async Task AddModuleAsync(Module module)
	{
		await AddTokenToRequestHeader();
		await _httpClient.PostAsJsonAsync("api/modules", module);
	}

	public async Task DeleteModuleAsync(int id)
	{
		await AddTokenToRequestHeader();
		await _httpClient.DeleteAsync($"api/modules/{id}");
	}
}
