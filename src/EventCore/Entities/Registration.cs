namespace EventCore.Entities;

public class Registration
{
    public int Id { get; private set; }
    public int EventId { get; private set; }
    public Event Event { get; private set; } = null!;
    public string ParticipantName { get; private set; } = string.Empty;
    public string ParticipantEmail { get; private set; } = string.Empty;
    public DateTime RegistrationDate { get; private set; }

    private Registration() { }

    // Internal constructor. Detta för att registreringar bara ska kunna skapas i EventCore, av
    // Event-klassen. 
    internal Registration(Event eventEntity, string participantName, string participantEmail)
    {
        if (eventEntity == null)
        {
            throw new ArgumentNullException(nameof(eventEntity));
        }

        if (string.IsNullOrWhiteSpace(participantName))
        {
            throw new ArgumentException("Participant name cannot be empty");
        }

        if (string.IsNullOrWhiteSpace(participantEmail))
        {
            throw new ArgumentException("Participant email cannot be empty");
        }

        if (!IsValidEmail(participantEmail))
        {
            throw new ArgumentException("Participant email is not valid");
        }

        Event = eventEntity;
        EventId = eventEntity.Id;
        ParticipantName = participantName;
        ParticipantEmail = participantEmail;
        RegistrationDate = DateTime.UtcNow;
    }

    private static bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }
}

