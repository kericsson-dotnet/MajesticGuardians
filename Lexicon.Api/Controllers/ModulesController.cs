using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lexicon.Api.Data;
using Lexicon.Api.Entities;

namespace Lexicon.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ModulesController : ControllerBase
{
    private readonly LexiconLmsContext _context;

    public ModulesController(LexiconLmsContext context)
    {
        _context = context;
    }

    // GET: api/Modules
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Module>>> GetModules()
    {
        return await _context.Modules.ToListAsync();
    }

    // GET: api/Modules/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Module>> GetModule(int id)
    {
        var @module = await _context.Modules.FindAsync(id);

        if (@module == null)
        {
            return NotFound();
        }

        return @module;
    }

    // PUT: api/Modules/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutModule(int id, Module @module)
    {
        if (id != @module.ModuleId)
        {
            return BadRequest();
        }

        _context.Entry(@module).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ModuleExists(id))
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

    // POST: api/Modules
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Module>> PostModule(Module @module)
    {
        _context.Modules.Add(@module);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetModule", new { id = @module.ModuleId }, @module);
    }

    // DELETE: api/Modules/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteModule(int id)
    {
        var @module = await _context.Modules.FindAsync(id);
        if (@module == null)
        {
            return NotFound();
        }

        _context.Modules.Remove(@module);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ModuleExists(int id)
    {
        return _context.Modules.Any(e => e.ModuleId == id);
    }
}
