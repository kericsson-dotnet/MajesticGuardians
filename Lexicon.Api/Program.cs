using Microsoft.EntityFrameworkCore;
using Lexicon.Api.Data;
using Lexicon.Api.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<LexiconLmsContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LexiconLmsContext") ?? throw new InvalidOperationException("Connection string 'LexiconLmsContext' not found.")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    FakeDataGenerator.Initialize(services);
}

app.MapControllers();

app.Run();