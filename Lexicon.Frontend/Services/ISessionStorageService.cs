namespace Lexicon.Frontend.Services;

public interface ISessionStorageService
{
    Task SetItemAsync(string key, string value);

    Task<string> GetItemAsync(string key);

    Task RemoveItemAsync(string key);
}