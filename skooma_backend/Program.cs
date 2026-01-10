using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

// CORS fürs Frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
    {
        policy
            .WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// URL nur fürs Öffnen im Browser
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

// KEIN HTTPS-Redirect, solange dein Backend auf HTTP läuft
// app.UseHttpsRedirection();

// Browser automatisch öffnen
app.Lifetime.ApplicationStarted.Register(() =>
{
    Process.Start(new ProcessStartInfo
    {
        FileName = url,
        UseShellExecute = true
    });
});

app.Run();
