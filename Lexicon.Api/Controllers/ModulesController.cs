using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lexicon.Api.Entities;
using Lexicon.Api.Repositories;
using AutoMapper;
using Lexicon.Api.Dtos.ModuleDtos;

namespace Lexicon.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ModulesController : ControllerBase
{
    private readonly IUnitOfWork _UoW;
    private readonly IMapper _mapper;

    public ModulesController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _UoW = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ModuleDto>>> GetModules()
    {
        var modules = await _UoW.Modules.GetAllAsync();

        if(modules == null || !modules.Any())
        {
            return NotFound();
        }

        return Ok(_mapper.Map<IEnumerable<ModuleDto>>(modules));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ModuleDto>> GetModule(int id)
    {
        try
        {
            var module = await _UoW.Modules.GetAsync(id);

            if(module == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ModuleDto>(module));
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutModule(int id, ModulePostDto modulePostDto)
    {
        var existingModule = await _UoW.Modules.GetAsync(id);

        if(existingModule == null)
        {
            return NotFound();
        }

        if (id != existingModule.ModuleId || id<=0)
        {
            return BadRequest();
        }

        _mapper.Map(modulePostDto, existingModule);

        try
        {
            _UoW.Modules.Update(existingModule);
            await _UoW.SaveAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (await _UoW.Modules.GetAsync(id) == null)
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
    public async Task<ActionResult<Module>> PostModule(ModulePostDto modulePostDto)
    {
        var existingModule = _mapper.Map<Module>(modulePostDto);

        try
        {
            _UoW.Modules.Add(existingModule);
            await _UoW.SaveAsync();
        }

        catch (Exception)
        {
            return StatusCode(500, "An error occured while posting the module");
        }

        return CreatedAtAction("GetModule", new { id = existingModule.ModuleId }, existingModule);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteModule(int id)
    {
        if (id <= 0)
        {
            return BadRequest();
        }

        var module = await _UoW.Modules.GetAsync(id);

        if (module == null)
        {
            return NotFound();
        }

        try
        {
            _UoW.Modules.Delete(module.ModuleId);
            await _UoW.SaveAsync();
        }

        catch (Exception)
        {
            return StatusCode(500, "An error occured while deleting the module");
        }


        return NoContent();
    }
}
