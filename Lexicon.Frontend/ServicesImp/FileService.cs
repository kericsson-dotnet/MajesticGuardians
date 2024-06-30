using Lexicon.Frontend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lexicon.Frontend.ServicesImp;

public class FileService : IFileService
{
	private readonly HttpClient _httpClient;
	public FileService(HttpClient httpClient)
	{
			_httpClient = httpClient;
		}
	public async Task<string> PostFileAsync(MultipartFormDataContent content)
	{
			try
			{
				var response = await _httpClient.PostAsync("/api/file/upload", content);
				if (response.IsSuccessStatusCode)
				{
					return "Upload successful!";
				}
				else
				{
					var errorMessage = await response.Content.ReadAsStringAsync();
					return $"Upload failed: {response.ReasonPhrase}. {errorMessage}";
				}
			}
			catch (Exception ex)
			{
				return $"Upload failed: {ex.Message}";
			}
		}
}