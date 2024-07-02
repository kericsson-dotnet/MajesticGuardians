using Lexicon.Frontend.Models;
using Lexicon.Frontend.Services;
using System.Net;
using System.Net.Http.Headers;

namespace Lexicon.Frontend.ServicesImp;

public class ActivityService : IActivityService
{
    private readonly HttpClient _httpClient;
	private readonly ISessionStorageService _sessionStorageService;

	public ActivityService(HttpClient httpClient, ISessionStorageService sessionStorageService)
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

	public async Task AddActivityAsync(Activity activity)
	{
		await AddTokenToRequestHeader();
		await _httpClient.PostAsJsonAsync("api/activities", activity);
	}

	public async Task DeleteActivityAsync(int id)
	{
		await AddTokenToRequestHeader();
		await _httpClient.DeleteAsync($"api/activities/{id}");
	}

	public async Task<List<Activity>> GetActivitiesAsync()
	{
		await AddTokenToRequestHeader();
		return await _httpClient.GetFromJsonAsync<List<Activity>>("api/activities");
	}

	public async Task<Activity> GetActivityAsync(int id)
	{
		await AddTokenToRequestHeader();

		try
		{
			return await _httpClient.GetFromJsonAsync<Activity>($"api/activities/{id}");
		}

		catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
		{
			return null;
		}
	}

	public async Task<bool> UpdateActivityAsync(int id, Activity activity)
	{
		await AddTokenToRequestHeader();

		try
		{
			// Send a PUT request and capture the response
			var res = await _httpClient.PutAsJsonAsync($"api/activities/{id}", activity);

			// If the request is unsuccessful, print detailed error information
			if (!res.IsSuccessStatusCode)
			{
				var responseContent = await res.Content.ReadAsStringAsync();
				Console.WriteLine($"Error: {res.StatusCode}");
				Console.WriteLine($"Reason: {res.ReasonPhrase}");
				Console.WriteLine($"Content: {responseContent}");
			}

			return res.IsSuccessStatusCode;
		}
		catch (Exception ex)
		{
			// Catching and handling exceptions
			Console.WriteLine($"Exception: {ex.Message}");
			return false;
		}
	}
}
