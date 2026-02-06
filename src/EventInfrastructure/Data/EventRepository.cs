using EventCore.Interfaces;
using EventCore.Entities;
using EventInfrastructure.Data;

public class EventRepository(EventDbContext context) : IEventRepository
{
    private readonly EventDbContext _context = context;

    public List<Event> GetAll()
    {
        return _context.Events.ToList();
    }
    public void Add(Event e)
    {
        _context.Events.Add(e);
        _context.SaveChanges();
    }
}