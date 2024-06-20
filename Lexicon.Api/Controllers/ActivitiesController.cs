using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lexicon.Api.Entities;
using Lexicon.Api.Repositories;

namespace Lexicon.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ActivitiesController : ControllerBase
{
    private readonly IUnitOfWork _UoW;

    public ActivitiesController(IUnitOfWork unitOfWork)
    {
        _UoW = unitOfWork;
    }

    // GET: api/Activities
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Activity>>> GetActivities()
    {
        var activities = await _UoW.Activities.GetAllAsync();
        return Ok(activities);
    }

    // GET: api/Activities/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Activity>> GetActivity(int id)
    {
        try
        {
            var activity = await _UoW.Activities.GetAsync(id);
            return Ok(activity);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
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

        try
        {
            _UoW.Activities.Update(activity);
            await _UoW.SaveAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (await _UoW.Activities.GetAsync(id) == null)
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
        _UoW.Activities.Add(activity);
        await _UoW.SaveAsync();

        return CreatedAtAction("GetActivity", new { id = activity.ActivityId }, activity);
    }

    // DELETE: api/Activities/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteActivity(int id)
    {
        var activity = await _UoW.Activities.GetAsync(id);
        if (activity == null)
        {
            return NotFound();
        }

        _UoW.Activities.Delete(activity);
        await _UoW.SaveAsync();

        return NoContent();
    }
}
