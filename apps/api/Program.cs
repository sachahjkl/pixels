using pixels_site.Api.Hubs;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddSignalR();

var app = builder.Build();
app.MapOpenApi();
app.MapScalarApiReference();

app.MapGet("/api/health", () => Results.Ok(new { ok = true }));

CanvasConfig.Width = int.Parse(Environment.GetEnvironmentVariable("CANVAS_WIDTH") ?? "1000");
CanvasConfig.Height = int.Parse(Environment.GetEnvironmentVariable("CANVAS_HEIGHT") ?? "1000");

app.MapHub<CanvasHub>("/hubs/canvas");





app.Run();
