using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lexicon.Api.Entities;
using Lexicon.Api.Repositories;
using AutoMapper;
using Lexicon.Api.Dtos.CourseDtos;

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
    public async Task<ActionResult<CourseDto>> GetCourse(int id)
    {
        if (id <= 0)
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

            return Ok(_mapper.Map<CourseDto>(course));
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> PutCourse(int id, CoursePostDto coursePost)
    {
        var existingCourse = await _UoW.Courses.GetAsync(id);

        if (existingCourse == null)
        {
            return NotFound();
        }

        if (id != existingCourse.CourseId || id <= 0)
        {
            return BadRequest();
        }

        _mapper.Map(coursePost, existingCourse);

        try
        {
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

        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<CoursePostDto>> PostCourse(CoursePostDto coursePostDto)
    {
        var course = _mapper.Map<Course>(coursePostDto);

        try
        {
            _UoW.Courses.Add(course);
            await _UoW.SaveAsync();
        }

        catch (Exception)
        {
            return StatusCode(500, "An error occured while posting the course");
        }

        return CreatedAtAction("GetCourse", new { id = course.CourseId }, course);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCourse(int id)
    {
        if (id <= 0)
        {
            return BadRequest();
        }

        var course = await _UoW.Courses.GetAsync(id);

        if (course == null)
        {
            return NotFound();
        }

        try
        {
            _UoW.Courses.Delete(course.CourseId);
            await _UoW.SaveAsync();
        }

        catch (Exception)
        {
            return StatusCode(500, "An error occured while deleting the course");
        }

        return NoContent();
    }
}
