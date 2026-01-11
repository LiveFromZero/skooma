using Microsoft.EntityFrameworkCore;
using skooma_backend.Data;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddScoped<AppDbContext>();


var app = builder.Build();
var url = "http://localhost:5000";

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
    url = "https://localhost:7101";
}

app.UseDefaultFiles(); // sucht index.html
app.UseStaticFiles();  // aktiviert wwwroot

app.MapFallbackToFile("index.html");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Lifetime.ApplicationStarted.Register(() =>
{
    Process.Start(new ProcessStartInfo
    {
        // f�r Ver�ffentlichung http://localhost:5000, f�rs Testen https://localhost:7101
        FileName = url,
        UseShellExecute = true
    });
});


using var scope = app.Services.CreateScope();


app.Run();
