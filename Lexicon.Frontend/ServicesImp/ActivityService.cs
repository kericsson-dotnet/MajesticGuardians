using Lexicon.Api.Dtos.ActivityDtos;
using Lexicon.Frontend.Models;
using Lexicon.Frontend.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace Lexicon.Frontend.ServicesImp;

public class ActivityService : IActivityService
{
    private readonly HttpClient _httpClient;

    public ActivityService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<Activity>> GetActivitiesAsync() => await _httpClient.GetFromJsonAsync<IEnumerable<Activity>>("api/activities");
    public async Task<Activity> GetActivityAsync(int id) => await _httpClient.GetFromJsonAsync<Activity>($"api/activities/{id}");
    public async Task<bool> PutActivityAsync(int id, Activity activity) {
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
