using Lexicon.Frontend.Models;
using Lexicon.Frontend.Services;

namespace Lexicon.Frontend.ServicesImp;

public class DocumentService : IDocumentService
{
    private readonly HttpClient _httpClient;

    public DocumentService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<Document>> GetDocumentsAsync() => await _httpClient.GetFromJsonAsync<IEnumerable<Document>>("api/documents");
    public async Task<Document> GetDocumentAsync(int id) => await _httpClient.GetFromJsonAsync<Document>($"api/documents/{id}");
	public async Task<string> PostFileAsync(MultipartFormDataContent content)
	{
		try
		{
			var response = await _httpClient.PostAsync("/api/documents/upload", content);
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
