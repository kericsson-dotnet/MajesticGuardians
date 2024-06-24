using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lexicon.Api.Entities;
using Lexicon.Api.Repositories;
using AutoMapper;
using Lexicon.Api.Dtos.UserDtos;

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
    public async Task<ActionResult<UserDto>> GetUser(int id)
    {
        try
        {
            var user = await _UoW.Users.GetAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<UserDto>(user));
        }

        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutUser(int id, UserPostDto userPostDto)
    {
        var existingUser = await _UoW.Users.GetAsync(id);

        if (existingUser == null)
        {
            return NotFound();
        }

        if (id != existingUser.UserId || id <= 0)
        {
            return BadRequest();
        }

        _mapper.Map(userPostDto, existingUser);

        try
        {
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

        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<UserPostDto>> PostUser(UserPostDto userPostDto)
    {
        var user = _mapper.Map<User>(userPostDto);

        try
        {
            _UoW.Users.Add(user);
            await _UoW.SaveAsync();
        }

        catch (Exception)
        {
            return StatusCode(500, "An error occured while posting the user");

        }
        return CreatedAtAction("GetUser", new { id = user.UserId }, user);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        if (id <= 0)
        {
            return BadRequest();
        }

        var user = await _UoW.Users.GetAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        try
        {
            _UoW.Users.Delete(user.UserId);
            await _UoW.SaveAsync();
        }

        catch (Exception)
        {
            return StatusCode(500, "An error occured while deleting the user");
        }

        return NoContent();
    }
}
