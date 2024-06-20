using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lexicon.Api.Entities;
using Lexicon.Api.Repositories;
using AutoMapper;
using Lexicon.Api.Dtos.ActivityDtos;

namespace Lexicon.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ActivitiesController : ControllerBase
{
    private readonly IUnitOfWork _UoW;
    private readonly IMapper _mapper;

    public ActivitiesController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _UoW = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetActivities()
    {
        var activities = await _UoW.Activities.GetAllAsync();

        if(activities == null || !activities.Any()) 
        {
            return BadRequest();
        }

        return Ok(_mapper.Map<IEnumerable<ActivityDto>>(activities));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Activity>> GetActivity(int id)
    {
        try
        {
            var activity = await _UoW.Activities.GetAsync(id);
            return Ok(_mapper.Map<ActivityDto>(activity));
        }

        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> PutActivity(int id, ActivityPostDto activityPostDto)
    {
        if (id <= 0 || !await ActivityExists(id))
        {
            return BadRequest();
        }

        var existingActivity = await _UoW.Activities.GetAsync(id);

        if (existingActivity == null)
        {
            return NotFound();
        }

        _mapper.Map(activityPostDto, existingActivity);

        try
        {
            _UoW.Activities.Update(existingActivity);
            await _UoW.SaveAsync();
        }

        catch (DbUpdateConcurrencyException)
        {
            if (!await ActivityExists(id))
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


    [HttpPost]
    public async Task<ActionResult<ActivityPostDto>> PostActivity(ActivityPostDto activityPostDto)
    {
        var activity = _mapper.Map<Activity>(activityPostDto); 
        try
        {
            _UoW.Activities.Add(activity);
            await _UoW.SaveAsync();
        }

        catch (InvalidOperationException)
        {
            return StatusCode(500, "An error occured while posting the activity");
        }

        return CreatedAtAction("GetActivity", new { id = activity.ActivityId }, activity);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteActivity(int id)
    {
        if (id <= 0)
        {
            return BadRequest();
        }

        var activity = await _UoW.Activities.GetAsync(id);

        if (activity == null)
        {
            return NotFound();
        }

        try
        {
            _UoW.Activities.Delete(activity);
            await _UoW.SaveAsync();
        }

        catch (Exception)
        {
            return StatusCode(500, "An error occured while deleting the activity");
        }

        return NoContent();
    }

    private async Task<bool> ActivityExists(int id) => await _UoW.Activities.GetAsync(id) != null;

}
