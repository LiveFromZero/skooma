using Microsoft.EntityFrameworkCore;
using skooma_backend.Data;
using skooma_backend.Models;
using skooma_backend.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=skooma.db";
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddHttpClient<MoonFetch>();
builder.Services.AddHttpClient<LaunchFetch>();

builder.Services.AddScoped<AnalysisCalculator>();
builder.Services.AddScoped<CacheService>();
builder.Services.AddScoped<MoonFetch>();
builder.Services.AddScoped<LaunchFetch>();

builder.Services.AddControllers();
builder.Services.AddMemoryCache(); // Register IMemoryCache service
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Ensure database is created
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated();
}

// Add mock data for testing the API
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (!dbContext.Launches.Any())
    {
        dbContext.Launches.AddRange(new[]
        {
            new Launch
            {
                Id = Guid.NewGuid().ToString(),
                LaunchDate = new DateTime(2025, 1, 15),
                Status = "Success",
                Location = new Location { CountryName = "USA"},
                RocketName = "Falcon 9"
            },
            new Launch
            {
                Id = Guid.NewGuid().ToString(),
                LaunchDate = new DateTime(2025, 2, 20),
                Status = "Failure",
                Location = new Location { CountryName = "Russia"},
                RocketName = "Soyuz"
            },
            new Launch
            {
                Id = Guid.NewGuid().ToString(),
                LaunchDate = new DateTime(2025, 3, 10),
                Status = "Success",
                Location = new Location { CountryName = "China"},
                RocketName = "Long March 5"
            }
        });
    }

    if (!dbContext.MoonData.Any())
    {
        dbContext.MoonData.AddRange(new[]
        {
            new MoonData {Id = Guid.NewGuid().ToString(), Date = new DateTime(2025, 1, 15), Phase = MoonPhase.Vollmond },
            new MoonData {Id = Guid.NewGuid().ToString(), Date = new DateTime(2025, 2, 20), Phase = MoonPhase.Neumond },
            new MoonData {Id = Guid.NewGuid().ToString(), Date = new DateTime(2025, 3, 10), Phase = MoonPhase.ZunehmenderHalbmond }
        });
    }

    dbContext.SaveChanges();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
