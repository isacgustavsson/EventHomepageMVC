using EventCore.Entities;

namespace UnitTests.Entities;

public class RegistrationTests
{
    private const string DefaultEventName = "Conference";
    private const string DefaultEventDescription = "Tech conference";
    private const string DefaultLocation = "Stockholm";
    private const int DefaultMaxParticipants = 100;

    private const string DefaultParticipantName = "John Doe";
    private const string DefaultParticipantEmail = "john@example.com";

    private static Event CreateValidEvent()
    {
        var startDate = DateTime.Now;
        var endDate = startDate.AddHours(2);
        return new Event(DefaultEventName, DefaultEventDescription, startDate, endDate, DefaultLocation, DefaultMaxParticipants);
    }

    [Fact]
    public void Constructor_WithValidData_CreatesRegistration()
    {
        // Arrange
        var evt = CreateValidEvent();

        // Act
        var registration = evt.RegisterParticipant(DefaultParticipantName, DefaultParticipantEmail);

        // Assert
        Assert.Equal(DefaultParticipantName, registration.ParticipantName);
        Assert.Equal(DefaultParticipantEmail, registration.ParticipantEmail);
        Assert.Equal(evt, registration.Event);
        Assert.NotEqual(default, registration.RegistrationDate);
    }

    [Fact]
    public void Constructor_WithEmptyName_ThrowsArgumentException()
    {
        // Arrange
        var evt = CreateValidEvent();

        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
            evt.RegisterParticipant("", DefaultParticipantEmail));
    }

    [Fact]
    public void Constructor_WithEmptyEmail_ThrowsArgumentException()
    {
        // Arrange
        var evt = CreateValidEvent();

        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
            evt.RegisterParticipant(DefaultParticipantName, ""));
    }

    [Fact]
    public void Constructor_WithInvalidEmail_ThrowsArgumentException()
    {
        // Arrange
        var evt = CreateValidEvent();

        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
            evt.RegisterParticipant(DefaultParticipantName, "not-an-email"));
    }

    [Fact]
    public void Constructor_SetsRegistrationDateToNow()
    {
        // Arrange
        var evt = CreateValidEvent();
        var before = DateTime.UtcNow;

        // Act
        var registration = evt.RegisterParticipant(DefaultParticipantName, DefaultParticipantEmail);
        var after = DateTime.UtcNow;

        // Assert
        Assert.InRange(registration.RegistrationDate, before, after);
    }

    [Fact]
    public void Constructor_WithValidEmail_AcceptsEmail()
    {
        // Arrange
        var evt = CreateValidEvent();
        const string emailWithPlus = "john.doe+test@example.com";

        // Act
        var registration = evt.RegisterParticipant(DefaultParticipantName, emailWithPlus);

        // Assert
        Assert.Equal(emailWithPlus, registration.ParticipantEmail);
    }
}
