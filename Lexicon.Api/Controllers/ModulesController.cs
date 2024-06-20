using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lexicon.Api.Entities;
using Lexicon.Api.Repositories;

namespace Lexicon.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ModulesController : ControllerBase
{
    private readonly IUnitOfWork _UoW;

    public ModulesController(IUnitOfWork unitOfWork)
    {
        _UoW = unitOfWork;
    }

    // GET: api/Modules
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Module>>> GetModules()
    {
        var modules = await _UoW.Modules.GetAllAsync();
        return Ok(modules);
    }

    // GET: api/Modules/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Module>> GetModule(int id)
    {
        try
        {
            var @module = await _UoW.Modules.GetAsync(id);
            return Ok(@module);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
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

        try
        {
            _UoW.Modules.Update(@module);
            await _UoW.SaveAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (await _UoW.Modules.GetAsync(id) == null)
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
        _UoW.Modules.Add(@module);
        await _UoW.SaveAsync();

        return CreatedAtAction("GetModule", new { id = @module.ModuleId }, @module);
    }

    // DELETE: api/Modules/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteModule(int id)
    {
        var @module = await _UoW.Modules.GetAsync(id);
        if (@module == null)
        {
            return NotFound();
        }

        _UoW.Modules.Delete(@module);
        await _UoW.SaveAsync();

        return NoContent();
    }
}
