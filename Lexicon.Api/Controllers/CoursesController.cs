using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lexicon.Api.Entities;
using Lexicon.Api.Repositories;

namespace Lexicon.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CoursesController : ControllerBase
{
    private readonly IUnitOfWork _UoW;

    public CoursesController(IUnitOfWork unitOfWork)
    {
        _UoW = unitOfWork;
    }

    // GET: api/Courses
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
    {
        var courses = await _UoW.Courses.GetAllAsync();
        return Ok(courses);
    }

    // GET: api/Courses/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Course>> GetCourse(int id)
    {
        try
        {
            var course = await _UoW.Courses.GetAsync(id);
            return Ok(course);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
    }

    // PUT: api/Courses/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCourse(int id, Course course)
    {
        if (id != course.CourseId)
        {
            return BadRequest();
        }

        try
        {
            _UoW.Courses.Update(course);
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

        return NoContent();
    }

    // POST: api/Courses
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Course>> PostCourse(Course course)
    {
        _UoW.Courses.Add(course);
        await _UoW.SaveAsync();

        return CreatedAtAction("GetCourse", new { id = course.CourseId }, course);
    }

    // DELETE: api/Courses/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCourse(int id)
    {
        var course = await _UoW.Courses.GetAsync(id);
        if (course == null)
        {
            return NotFound();
        }

        _UoW.Courses.Delete(course);
        await _UoW.SaveAsync();

        return NoContent();
    }
}
