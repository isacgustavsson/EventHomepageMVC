using EventCore.Entities;

namespace EventCore.Interfaces;

public interface IEventService
{
    public void Create(EventDto dto);
    public List<Event> GetAll();
}