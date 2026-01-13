using Microsoft.EntityFrameworkCore;
using skooma_backend.Data;
using skooma_backend.Data.DBServices;
using skooma_backend.Services;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddScoped<AppDbContext>();
builder.Services.AddScoped<LocationService>();
builder.Services.AddScoped<LaunchService>();
builder.Services.AddScoped<MoonDataService>();
builder.Services.AddHttpClient<LaunchFetch>();
builder.Services.AddHttpClient<MoonFetch>();
builder.Services.AddScoped<DBUpdateService>();

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

// URL nur f�rs �ffnen im Browser
var url = "http://localhost:5000";

// Middleware-Reihenfolge ist entscheidend
app.UseCors("FrontendPolicy");

if (app.Environment.IsDevelopment())
{
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


using var scope = app.Services.CreateScope();


app.Run();
