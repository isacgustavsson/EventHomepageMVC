using EventCore.Interfaces;
using EventCore.Entities;
public class EventService(IEventRepository repo) : IEventService
{
    private readonly IEventRepository _repo = repo;

    public List<Event> GetAll()
    {
        return _repo.GetAll();
    }

    public void Create(EventDto dto)
    {
        var e = new Event(
            dto.Name,
            dto.Description,
            dto.StartDateTime,
            dto.EndDateTime,
            dto.Location,
            dto.MaxParticipants
            );

        _repo.Add(e);
    }
}