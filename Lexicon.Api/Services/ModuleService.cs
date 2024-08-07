﻿using Lexicon.Api.Entities;
using Lexicon.Api.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Lexicon.Api.Services;

public class ModuleService(DbContext context) : CrudService<Module>(context), IModuleRepository
{
    public virtual async Task<IEnumerable<Module>> GetAllAsync()
    {
        return await _context.Set<Module>()
            .Include(m => m.Activities)
            .Include(m => m.Documents)
            .ToListAsync();
    }

    public virtual async Task<Module> GetAsync(object id)
    {
        if (id is int moduleId)
        {
            return await _context.Set<Module>()
                       .Include(m => m.Activities)
                       .Include(m => m.Documents)
                       .FirstOrDefaultAsync(m => m.ModuleId == moduleId) ??
                   throw new InvalidOperationException($"{typeof(Module).Name} Id {id} not found.");
        }

        throw new ArgumentException("Invalid ID type");
    }
}