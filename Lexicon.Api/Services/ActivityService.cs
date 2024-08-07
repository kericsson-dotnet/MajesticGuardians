﻿using Lexicon.Api.Entities;
using Lexicon.Api.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Lexicon.Api.Services;

public class ActivityService(DbContext context) : CrudService<Activity>(context), IActivityRepository
{
    public virtual async Task<IEnumerable<Activity>> GetAllAsync()
    {
        return await _context.Set<Activity>()
            .Include(a => a.Documents)
            .ToListAsync();
    }

    public virtual async Task<Activity> GetAsync(object id)
    {
        if (id is int activityId)
        {
            return await _context.Set<Activity>()
                       .Include(a => a.Documents)
                       .FirstOrDefaultAsync(a => a.ActivityId == activityId) ??
                   throw new InvalidOperationException($"{typeof(Activity).Name} Id {id} not found.");
        }

        throw new ArgumentException("Invalid ID type");
    }
}