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
[Authorize(Roles = "Teacher, Student")]
public class UsersController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UsersController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
	public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
    {
        var users = await _unitOfWork.Users.GetAllAsync();

        if (users == null || !users.Any())
        {
            return BadRequest();
        }

        return Ok(_mapper.Map<IEnumerable<UserDto>>(users));
    }

    [HttpGet("{id}")]
	public async Task<ActionResult<UserDto>> GetUser([FromRoute] int id)
    {
        try
        {
            var user = await _unitOfWork.Users.GetAsync(id);
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
            var existingUser = await _unitOfWork.Users.GetAsync(id);

            if (existingUser == null)
            {
                return NotFound();
            }

            if (id <= 0)
            {
                return BadRequest();
            }

            _mapper.Map(userPostDto, existingUser);

            _unitOfWork.Users.Update(existingUser);
            await _unitOfWork.SaveAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (await _unitOfWork.Users.GetAsync(id) == null)
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
            _unitOfWork.Users.Add(user);
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

        return CreatedAtAction("GetUser", new { id = user.UserId }, user);
    }

    [HttpDelete("{id}")]
	public async Task<IActionResult> DeleteUser([FromRoute] int id)
    {
        if (id <= 0)
        {
            return BadRequest();
        }

        try
        {
            var user = await _unitOfWork.Users.GetAsync(id);
            _unitOfWork.Users.Delete(user.UserId);
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
}
