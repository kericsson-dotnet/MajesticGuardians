using Microsoft.EntityFrameworkCore;
using Lexicon.Api.Entities;

namespace Lexicon.Api.Data;

public class LexiconLmsContext(DbContextOptions<LexiconLmsContext> options) : DbContext(options)
{

    public DbSet<User> Users { get; set; } = default!;
    public DbSet<Course> Courses { get; set; } = default!;
    public DbSet<Activity> Activities { get; set; } = default!;
    public DbSet<Document> Documents { get; set; } = default!;
    public DbSet<Module> Modules { get; set; } = default!;
}
