using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

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
        // für Veröffentlichung http://localhost:5000, fürs Testen https://localhost:7101
        FileName = url,
        UseShellExecute = true
    });
});

app.Run();
