using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lexicon.Api.Data;
using Lexicon.Api.Entities;

namespace Lexicon.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ActivitiesController : ControllerBase
{
    private readonly LexiconLmsContext _context;

    public ActivitiesController(LexiconLmsContext context)
    {
        _context = context;
    }

    // GET: api/Activities
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Activity>>> GetActivities()
    {
        return await _context.Activities.ToListAsync();
    }

    // GET: api/Activities/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Activity>> GetActivity(int id)
    {
        var activity = await _context.Activities.FindAsync(id);

        if (activity == null)
        {
            return NotFound();
        }

        return activity;
    }

    // PUT: api/Activities/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutActivity(int id, Activity activity)
    {
        if (id != activity.ActivityId)
        {
            return BadRequest();
        }

        _context.Entry(activity).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ActivityExists(id))
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

    // POST: api/Activities
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Activity>> PostActivity(Activity activity)
    {
        _context.Activities.Add(activity);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetActivity", new { id = activity.ActivityId }, activity);
    }

    // DELETE: api/Activities/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteActivity(int id)
    {
        var activity = await _context.Activities.FindAsync(id);
        if (activity == null)
        {
            return NotFound();
        }

        _context.Activities.Remove(activity);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ActivityExists(int id)
    {
        return _context.Activities.Any(e => e.ActivityId == id);
    }
}
