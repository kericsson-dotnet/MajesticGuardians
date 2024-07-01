using Lexicon.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lexicon.Api.Repositories;

public class DocumentRepository(DbContext context) : CrudRepository<Document>(context), IDocumentRepository;