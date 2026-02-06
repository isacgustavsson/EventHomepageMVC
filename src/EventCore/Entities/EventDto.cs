namespace EventCore.Entities;

public record EventDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
    public string Location { get; set; } = string.Empty;
    public int? MaxParticipants { get; set; }


    private EventDto() { } // Behövs för EF Core

    public EventDto(string name, string description, DateTime startDateTime, DateTime endDateTime, string location, int? maxParticipants = null)
    {
        Name = name;
        Description = description;
        StartDateTime = startDateTime;
        EndDateTime = endDateTime;
        Location = location;
        MaxParticipants = maxParticipants;
    }

}