using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lexicon.Api.Entities;
using Lexicon.Api.Repositories;
using AutoMapper;
using Lexicon.Api.Dtos.ActivityDtos;
using Microsoft.AspNetCore.Authorization;

namespace Lexicon.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Teacher, Student")]
public class ActivitiesController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ActivitiesController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
	public async Task<IActionResult> GetActivities()
    {
        var activities = await _unitOfWork.Activities.GetAllAsync();

        if (activities == null || !activities.Any())
        {
            return BadRequest();
        }

        return Ok(_mapper.Map<IEnumerable<ActivityDto>>(activities));
    }

    [HttpGet("{id}")]
	public async Task<ActionResult<ActivityDto>> GetActivity([FromRoute] int id)
    {
        try
        {
            var activity = await _unitOfWork.Activities.GetAsync(id);
            return Ok(_mapper.Map<ActivityDto>(activity));
        }

        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }

        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPut("{id}")]
	public async Task<IActionResult> PutActivity([FromRoute] int id, [FromBody] ActivityPostDto activityPostDto)
    {
        if (id <= 0)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var existingActivity = await _unitOfWork.Activities.GetAsync(id);
			_mapper.Map(activityPostDto, existingActivity);
            _unitOfWork.Activities.Update(existingActivity);
            await _unitOfWork.SaveAsync();
			return NoContent();
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

        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }

        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }

    }

    [HttpPost]
	public async Task<ActionResult<ActivityPostDto>> PostActivity([FromBody] ActivityPostDto activityPostDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var activity = _mapper.Map<Activity>(activityPostDto);
        
        try
        {
            _unitOfWork.Activities.Add(activity);
            await _unitOfWork.SaveAsync();
        }

        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }

        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }


        return CreatedAtAction("GetActivity", new { id = activity.ActivityId }, activity);
    }

    [HttpDelete("{id}")]
	public async Task<IActionResult> DeleteActivity([FromRoute] int id)
    {
        if (id <= 0)
        {
            return BadRequest();
        }

        try
        {
            var activity = await _unitOfWork.Activities.GetAsync(id);
            _unitOfWork.Activities.Delete(activity.ActivityId);
            await _unitOfWork.SaveAsync();
        }

        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }

        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }

        return NoContent();
    }

    private async Task<bool> ActivityExists(int id) => await _unitOfWork.Activities.GetAsync(id) != null;

}
