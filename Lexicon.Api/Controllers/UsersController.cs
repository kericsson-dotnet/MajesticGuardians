using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lexicon.Api.Entities;
using Lexicon.Api.Repositories;
using AutoMapper;
using Lexicon.Api.Dtos.UserDtos;
using Microsoft.AspNetCore.Authorization;

namespace Lexicon.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUnitOfWork _UoW;
    private readonly IMapper _mapper;


    public UsersController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _UoW = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [Authorize(Roles = "Teacher")]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
    {
        var users = await _UoW.Users.GetAllAsync();

        if (users == null || !users.Any())
        {
            return BadRequest();
        }

        return Ok(_mapper.Map<IEnumerable<UserDto>>(users));
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Teacher, Student")]
    public async Task<ActionResult<UserDto>> GetUser([FromRoute] int id)
    {
        try
        {
            var user = await _UoW.Users.GetAsync(id);
            return Ok(_mapper.Map<UserDto>(user));
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
    [Authorize(Roles = "Teacher")]
    public async Task<IActionResult> PutUser([FromRoute] int id, [FromBody] UserPostDto userPostDto)
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
            var existingUser = await _UoW.Users.GetAsync(id);

            if (existingUser == null)
            {
                return NotFound();
            }

            if (id <= 0)
            {
                return BadRequest();
            }

            _mapper.Map(userPostDto, existingUser);

            _UoW.Users.Update(existingUser);
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
    public async Task<ActionResult<UserPostDto>> PostUser([FromBody] UserPostDto userPostDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = _mapper.Map<User>(userPostDto);

        try
        {
            _UoW.Users.Add(user);
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

        return CreatedAtAction("GetUser", new { id = user.UserId }, user);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Teacher")]
    public async Task<IActionResult> DeleteUser([FromRoute] int id)
    {
        if (id <= 0)
        {
            return BadRequest();
        }

        try
        {
            var user = await _UoW.Users.GetAsync(id);
            _UoW.Users.Delete(user.UserId);
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
}
