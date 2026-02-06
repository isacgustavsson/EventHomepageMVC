using EventInfrastructure.Data;
using Microsoft.EntityFrameworkCore;
using EventCore.Interfaces;
using EventUI.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<EventDbContext>(options =>
options.UseSqlite(@"Data Source=event.db"));

builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IEventService, EventService>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapEventEndpoints();


app.Run();
