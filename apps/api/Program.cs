using pixels_site.Api.Canvas;
using pixels_site.Api.Hubs;
using Scalar.AspNetCore;

CanvasConfig.Width = int.Parse(Environment.GetEnvironmentVariable("CANVAS_WIDTH") ?? "1000");
CanvasConfig.Height = int.Parse(Environment.GetEnvironmentVariable("CANVAS_HEIGHT") ?? "1000");

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddSignalR();
builder.Services.AddSingleton<CanvasStateService>();

var app = builder.Build();

app.MapOpenApi();
app.MapScalarApiReference();

app.MapGet("/api/health", () => Results.Ok(new { ok = true }));
app.MapGet("/api/canvas/config", () => Results.Ok(new { width = CanvasConfig.Width, height = CanvasConfig.Height }));
app.MapGet("/api/canvas/snapshot", (CanvasStateService canvasState) =>
    Results.Bytes(canvasState.GetSnapshot(), "application/octet-stream"));

app.MapHub<CanvasHub>("/hubs/canvas");

app.Run();
