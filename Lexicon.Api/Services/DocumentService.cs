using Lexicon.Api.Entities;
using Lexicon.Api.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Lexicon.Api.Services;

public class DocumentService(DbContext context) : CrudService<Document>(context), IDocumentRepository;