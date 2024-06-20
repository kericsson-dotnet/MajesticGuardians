using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lexicon.Api.Entities;
using Lexicon.Api.Repositories;

namespace Lexicon.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DocumentsController : ControllerBase
{
    private readonly IUnitOfWork _UoW;

    public DocumentsController(IUnitOfWork unitOfWork)
    {
        _UoW = unitOfWork;
    }

    // GET: api/Documents
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Document>>> GetDocuments()
    {
        var documents = await _UoW.Documents.GetAllAsync();
        return Ok(documents);
    }

    // GET: api/Documents/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Document>> GetDocument(int id)
    {
        try
        {
            var document = await _UoW.Documents.GetAsync(id);
            return Ok(document);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
    }

    // PUT: api/Documents/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutDocument(int id, Document document)
    {
        if (id != document.DocumentId)
        {
            return BadRequest();
        }

        try
        {
            _UoW.Documents.Update(document);
            await _UoW.SaveAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (await _UoW.Documents.GetAsync(id) == null)
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/Documents
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Document>> PostDocument(Document document)
    {
        _UoW.Documents.Add(document);
        await _UoW.SaveAsync();

        return CreatedAtAction("GetDocument", new { id = document.DocumentId }, document);
    }

    // DELETE: api/Documents/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDocument(int id)
    {
        var document = await _UoW.Documents.GetAsync(id);
        if (document == null)
        {
            return NotFound();
        }

        _UoW.Documents.Delete(document);
        await _UoW.SaveAsync();

        return NoContent();
    }
}
