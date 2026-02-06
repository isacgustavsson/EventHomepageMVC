using EventCore.Entities;

namespace EventCore.Interfaces;

public interface IEventRepository
{
    public List<Event> GetAll();
    public void Add(Event e);
}