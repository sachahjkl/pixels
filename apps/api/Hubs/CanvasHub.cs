using Microsoft.AspNetCore.SignalR;

namespace pixels_site.Api.Hubs;

public class CanvasHub : Hub
{
    public async Task PlacePixel(PixelPlacementRequest request)
    {
        if (request.X < 0 || request.X >= 100 || request.Y < 0 || request.Y >= 100)
        {
            throw new HubException("Invalid pixel coordinates");
        }

        if (string.IsNullOrWhiteSpace(request.Color) || !IsValidHexColor(request.Color))
        {
            throw new HubException("Invalid color");
        }

        var pixel = new PixelPlacedEvent(request.X, request.Y, request.Color, DateTime.UtcNow);

        await Clients.All.SendAsync("PixelPlaced", pixel);
    }

    private static bool IsValidHexColor(string color)
    {
        if (string.IsNullOrWhiteSpace(color)) return false;
        if (color.Length != 7 || color[0] != '#') return false;
        return color[1..].All(c => "0123456789abcdefABCDEF".Contains(c));
    }
}

public record PixelPlacementRequest(int X, int Y, string Color);
public record PixelPlacedEvent(int X, int Y, string Color, DateTime PlacedAtUtc);
