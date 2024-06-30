namespace Lexicon.Frontend.Services;

public interface IFileService
{
	Task<string> PostFileAsync(MultipartFormDataContent content);
}