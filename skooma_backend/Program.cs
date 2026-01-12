using Microsoft.EntityFrameworkCore;
using skooma_backend.Data;
using skooma_backend.Models;
using skooma_backend.Services;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Services
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

// CORS f�rs Frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
    {
        policy
            .WithOrigins("http://localhost:5212")
            // for testing with "run dev" in vite: http://localhost:5173
            // for using within visual studio/with backend: http://localhost:5212
            // for production/before building: http://localhost:5000
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// Ensure database is created
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated();
}


// Configure the HTTP request pipeline.
// URL nur f�rs �ffnen im Browser
var url = "http://localhost:5000";

// Middleware-Reihenfolge ist entscheidend
app.UseCors("FrontendPolicy");

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();

    // nur Anzeige, kein Routing
    url = "http://localhost:5212";
}

// API zuerst
app.MapControllers();

// SPA danach
app.UseDefaultFiles();
app.UseStaticFiles();
app.MapFallbackToFile("index.html");

// KEIN HTTPS-Redirect, solange dein Backend auf HTTP l�uft
// app.UseHttpsRedirection();

// Browser automatisch �ffnen
app.Lifetime.ApplicationStarted.Register(() =>
{
    Process.Start(new ProcessStartInfo
    {
        FileName = url,
        UseShellExecute = true
    });
});

app.Run();
