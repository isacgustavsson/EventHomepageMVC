using Microsoft.AspNetCore.Mvc;
using EventCore.Entities;
using EventCore.Interfaces;

namespace EventUI.Extensions;

public static class EventRoutes
{
    public static void MapEventEndpoints(this WebApplication app)
    {

        app.MapGet("/events", (IEventService service) =>
        {
            var events = service.GetAll();
            return Results.Json(events);
        });

        app.MapPost("/events", ([FromBody] EventDto dto, [FromServices] IEventService service) =>
        {
            service.Create(dto);
            return Results.Ok();
        });
    }
}
