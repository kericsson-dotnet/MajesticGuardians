using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lexicon.Api.Entities;
using Lexicon.Api.Repositories;
using AutoMapper;
using Lexicon.Api.Dtos.ModuleDtos;
using Microsoft.AspNetCore.Authorization;

namespace Lexicon.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Teacher, Student")]
public class ModulesController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ModulesController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
	public async Task<ActionResult<IEnumerable<ModuleDto>>> GetModules()
    {
        var modules = await _unitOfWork.Modules.GetAllAsync();

        if(modules == null || !modules.Any())
        {
            return NotFound();
        }

        return Ok(_mapper.Map<IEnumerable<ModuleDto>>(modules));
    }

    [HttpGet("{id}")]
	public async Task<ActionResult<ModuleDto>> GetModule([FromRoute] int id)
    {
        try
        {
            var module = await _unitOfWork.Modules.GetAsync(id);
            return Ok(_mapper.Map<ModuleDto>(module));
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
	public async Task<IActionResult> PutModule([FromRoute] int id, [FromBody] ModulePostDto modulePostDto)
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
            var existingModule = await _unitOfWork.Modules.GetAsync(id);
            _mapper.Map(modulePostDto, existingModule);
            _unitOfWork.Modules.Update(existingModule);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (await _unitOfWork.Modules.GetAsync(id) == null)
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
	public async Task<ActionResult<ModulePostDto>> PostModule([FromBody] ModulePostDto modulePostDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var existingModule = _mapper.Map<Module>(modulePostDto);

        try
        {
            _unitOfWork.Modules.Add(existingModule);
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

        return CreatedAtAction("GetModule", new { id = existingModule.ModuleId }, existingModule);
    }

    [HttpDelete("{id}")]
	public async Task<IActionResult> DeleteModule([FromRoute] int id)
    {
        if (id <= 0)
        {
            return BadRequest();
        }

        try
        {
            var module = await _unitOfWork.Modules.GetAsync(id);
            _unitOfWork.Modules.Delete(module.ModuleId);
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
