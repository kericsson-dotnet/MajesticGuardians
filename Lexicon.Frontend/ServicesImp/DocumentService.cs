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
}
