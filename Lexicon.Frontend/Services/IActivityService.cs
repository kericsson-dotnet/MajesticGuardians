using Lexicon.Frontend.Models;

namespace Lexicon.Frontend.Services;

public interface IActivityService
{
    Task<List<Activity>> GetActivitiesAsync();
    
    Task<Activity> GetActivityAsync(int id);

	Task<bool> UpdateActivityAsync(int id, Activity activity);
    
    Task AddActivityAsync(Activity activity);

    Task DeleteActivityAsync(int id);
}
