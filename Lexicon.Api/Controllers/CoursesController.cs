using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lexicon.Api.Entities;
using Lexicon.Api.Repositories;
using AutoMapper;
using Lexicon.Api.Dtos.CourseDtos;
using Lexicon.Api.Dtos.UserDtos;

namespace Lexicon.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CoursesController : ControllerBase
{
    private readonly IUnitOfWork _UoW;
    private readonly IMapper _mapper;

    public CoursesController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _UoW = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CourseDto>>> GetCourses()
    {
        var courses = await _UoW.Courses.GetAllAsync();

        if (courses == null || !courses.Any())
        {
            return BadRequest();
        }

        return Ok(_mapper.Map<IEnumerable<CourseDto>>(courses));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CourseDto>> GetCourse([FromRoute] int id)
    {
        if (id <= 0)
        {
            return BadRequest();
        }

        try
        {
            var course = await _UoW.Courses.GetAsync(id);

            return Ok(_mapper.Map<CourseDto>(course));
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
    public async Task<IActionResult> PutCourse([FromRoute] int id, [FromBody] CoursePostDto coursePost)
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
            var existingCourse = await _UoW.Courses.GetAsync(id);
            _mapper.Map(coursePost, existingCourse);

            _UoW.Courses.Update(existingCourse);
            await _UoW.SaveAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (await _UoW.Users.GetAsync(id) == null)
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

        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<CoursePostDto>> PostCourse([FromBody] CoursePostDto coursePostDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var course = _mapper.Map<Course>(coursePostDto);

        try
        {
            _UoW.Courses.Add(course);
            await _UoW.SaveAsync();
        }

        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }

        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }

        return CreatedAtAction("GetCourse", new { id = course.CourseId }, course);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCourse([FromRoute] int id)
    {
        if (id <= 0)
        {
            return BadRequest();
        }

        try
        {
            var course = await _UoW.Courses.GetAsync(id);

            _UoW.Courses.Delete(course.CourseId);
            await _UoW.SaveAsync();
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

    [HttpGet("{id}/getAllUsersInCourse")]
    public async Task<ActionResult<IEnumerable<UserWithIdDto>>> GetAllUsersInCourse([FromRoute] int id)
    {
        if (id <= 0)
        {
            return BadRequest();
        }

        try
        {
            var users = await _UoW.Courses.GetAllUsersInCourse(id);

            if (users == null || !users.Any())
            {
                return NotFound("No users found for the course.");
            }

            return Ok(_mapper.Map<IEnumerable<UserWithIdDto>>(users));
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

    [HttpPost("{id}/addUserToCourse/{userId}")]
    public async Task<IActionResult> AddUserToCourse([FromRoute] int id, [FromRoute] int userId)
    {
        if (id <= 0 || userId <= 0)
        {
            return BadRequest();
        }

        try
        {
            var course = await _UoW.Courses.GetAsync(id);

            if (course == null)
            {
                return NotFound();
            }

            var user = await _UoW.Users.GetAsync(userId);

            if (user == null)
            {
                return NotFound($"User with id {userId} not found.");
            }

            _UoW.Courses.AddUserToCourse(id, _mapper.Map<User>(user));
            await _UoW.SaveAsync();

            return Ok($"User with id {userId} successfully added to course {id}.");
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

    [HttpDelete("{id}/removeUserFromCourse/{userId}")]
    public async Task<IActionResult> RemoveUserFromCourse([FromRoute] int id, [FromRoute] int userId)
    {
        if (id <= 0 || userId <= 0)
        {
            return BadRequest();
        }

        try
        {
            _UoW.Courses.RemoveUserFromCourse(id, userId);
            await _UoW.SaveAsync();
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

    [HttpGet("{id}/getAllAvailableUserForCourse")]
    public async Task<ActionResult<IEnumerable<UserWithIdDto>>> GetAllAvailableUserForCourse([FromRoute] int id)
    {
        if (id <= 0)
        {
            return BadRequest();
        }

        try
        {
            var users = await _UoW.Courses.GetAllAvailableUserForCourse(id);

            if (users == null || !users.Any())
            {
                return NotFound("No users found for the course.");
            }

            return Ok(_mapper.Map<IEnumerable<UserWithIdDto>>(users));
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

    [HttpGet("getAllUsersCourses/{userId}")]
    public async Task<ActionResult<IEnumerable<CourseWithIdDto>>> GetAllUsersCourses([FromRoute] int userId)
    {
        if (userId <= 0)
        {
            return BadRequest();
        }

        try
        {
            var users = await _UoW.Courses.GetAllUserCourses(userId);

            if (users == null || !users.Any())
            {
                return NotFound($"User with Id {userId} has no courses");
            }

            return Ok(_mapper.Map<IEnumerable<CourseWithIdDto>>(users));
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

}