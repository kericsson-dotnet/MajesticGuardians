using Lexicon.Frontend.Models;

namespace Lexicon.Frontend.Services;

public interface IActivityService
{
    Task<IEnumerable<Activity>> GetActivitiesAsync();
    Task<Activity> GetActivityAsync(int id);
	Task<bool> PutActivityAsync(int id, Activity activity);
}
