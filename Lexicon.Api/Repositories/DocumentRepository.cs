using Lexicon.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lexicon.Api.Repositories;

public class DocumentRepository : CrudRepository<Document>, IDocumentRepository
{
    public DocumentRepository(DbContext context) : base(context)
    {

        }
}