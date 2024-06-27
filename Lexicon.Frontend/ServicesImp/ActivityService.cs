using Lexicon.Api.Dtos.ActivityDtos;
using Lexicon.Frontend.Models;
using Lexicon.Frontend.Services;
using Microsoft.AspNetCore.Mvc;

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
        var res = await _httpClient.PutAsJsonAsync($"api/activities/{id}", activity);
        return res.IsSuccessStatusCode;
	}
}
