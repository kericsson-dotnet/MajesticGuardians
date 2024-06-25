using Lexicon.Frontend.Models;

namespace Lexicon.Frontend.Services;

public interface IDocumentService
{
    Task<IEnumerable<Document>> GetDocumentsAsync();
}
