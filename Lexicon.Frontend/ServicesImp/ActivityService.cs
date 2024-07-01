using Lexicon.Frontend.Models;
using Lexicon.Frontend.Services;

namespace Lexicon.Frontend.ServicesImp;

public class ActivityService : IActivityService
{
    private readonly HttpClient _httpClient;

    public ActivityService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

	public async Task AddActivityAsync(Activity activity) => await _httpClient.PostAsJsonAsync("api/activities", activity);

	public async Task DeleteActivityAsync(int id) => await _httpClient.DeleteAsync($"api/activities/{id}");

    public async Task<List<Activity>> GetActivitiesAsync() => await _httpClient.GetFromJsonAsync<List<Activity>>("api/activities");

    public async Task<Activity> GetActivityAsync(int id) => await _httpClient.GetFromJsonAsync<Activity>($"api/activities/{id}");
    
	public async Task<bool> UpdateActivityAsync(int id, Activity activity) {
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
