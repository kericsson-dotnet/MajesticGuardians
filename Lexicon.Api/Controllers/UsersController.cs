using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lexicon.Api.Entities;
using Lexicon.Api.Repositories;

namespace Lexicon.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUnitOfWork _UoW;

    public UsersController(IUnitOfWork unitOfWork)
    {
        _UoW = unitOfWork;
    }

    // GET: api/Users
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        var users = await _UoW.Users.GetAllAsync();
        return Ok(users);
    }

    // GET: api/Users/5
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        try
        {
            var user = await _UoW.Users.GetAsync(id);
            return Ok(user);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
    }

    // PUT: api/Users/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutUser(int id, User user)
    {
        if (id != user.UserId)
        {
            return BadRequest();
        }

        try
        {
            _UoW.Users.Update(user);
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

    // POST: api/Users
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<User>> PostUser(User user)
    {
        _UoW.Users.Add(user);
        await _UoW.SaveAsync();

        return CreatedAtAction("GetUser", new { id = user.UserId }, user);
    }

    // DELETE: api/Users/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = await _UoW.Users.GetAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        _UoW.Users.Delete(user);
        await _UoW.SaveAsync();

        return NoContent();
    }
}
