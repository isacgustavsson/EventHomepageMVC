using EventCore.Entities;
using EventCore.Exceptions;

namespace UnitTests.Entities;

public class EventTests
{
    private const string DefaultEventName = "Conference";
    private const string DefaultEventDescription = "Tech conference";
    private const string DefaultLocation = "Stockholm";
    private const int DefaultMaxParticipants = 100;

    private const string DefaultParticipantName = "John Doe";
    private const string DefaultParticipantEmail = "john@example.com";
    private const string AlternateParticipantName = "Jane Smith";
    private const string AlternateParticipantEmail = "jane@example.com";

    private static Event CreateValidEvent(int? maxParticipants = DefaultMaxParticipants)
    {
        var (startDate, endDate) = GetValidDates();
        return new Event(DefaultEventName, DefaultEventDescription, startDate, endDate, DefaultLocation, maxParticipants);
    }

    private static (DateTime start, DateTime end) GetValidDates()
    {
        var start = DateTime.Now;
        return (start, start.AddHours(2));
    }

    [Fact]
    public void Constructor_WithValidData_CreatesEvent()
    {
        // Arrange & Act
        var evt = CreateValidEvent();

        // Assert
        Assert.Equal(DefaultEventName, evt.Name);
        Assert.Equal(DefaultEventDescription, evt.Description);
        Assert.Equal(DefaultLocation, evt.Location);
        Assert.Equal(DefaultMaxParticipants, evt.MaxParticipants);
        Assert.Empty(evt.Registrations);
    }

    [Fact]
    public void Constructor_WithEmptyName_ThrowsArgumentException()
    {
        // Arrange
        var (startDate, endDate) = GetValidDates();

        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
            new Event("", DefaultEventDescription, startDate, endDate, DefaultLocation, DefaultMaxParticipants));
    }

    [Fact]
    public void Constructor_WithEndDateBeforeStartDate_ThrowsArgumentException()
    {
        // Arrange
        var startDate = DateTime.Now;
        var endDate = startDate.AddHours(-2);

        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
            new Event(DefaultEventName, DefaultEventDescription, startDate, endDate, DefaultLocation, DefaultMaxParticipants));
    }

    [Fact]
    public void Constructor_WithEmptyLocation_ThrowsArgumentException()
    {
        // Arrange
        var (startDate, endDate) = GetValidDates();

        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
            new Event(DefaultEventName, DefaultEventDescription, startDate, endDate, "", DefaultMaxParticipants));
    }

    [Fact]
    public void Constructor_WithNegativeMaxParticipants_ThrowsArgumentException()
    {
        // Arrange
        var (startDate, endDate) = GetValidDates();

        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
            new Event(DefaultEventName, DefaultEventDescription, startDate, endDate, DefaultLocation, -1));
    }

    [Fact]
    public void IsFull_WithNoMaxParticipants_ReturnsFalse()
    {
        // Arrange
        var evt = CreateValidEvent(maxParticipants: null);

        // Act & Assert
        Assert.False(evt.IsFull());
    }

    [Fact]
    public void IsFull_WithRegistrationsBelowMax_ReturnsFalse()
    {
        // Arrange
        var evt = CreateValidEvent(maxParticipants: 3);
        evt.RegisterParticipant(DefaultParticipantName, DefaultParticipantEmail);

        // Act & Assert
        Assert.False(evt.IsFull());
    }

    [Fact]
    public void IsFull_WithRegistrationsAtMax_ReturnsTrue()
    {
        // Arrange
        var evt = CreateValidEvent(maxParticipants: 2);
        evt.RegisterParticipant(DefaultParticipantName, DefaultParticipantEmail);
        evt.RegisterParticipant(AlternateParticipantName, AlternateParticipantEmail);

        // Act & Assert
        Assert.True(evt.IsFull());
    }

    [Fact]
    public void RegisterParticipant_WithValidData_AddsRegistration()
    {
        // Arrange
        var evt = CreateValidEvent();

        // Act
        var registration = evt.RegisterParticipant(DefaultParticipantName, DefaultParticipantEmail);

        // Assert
        Assert.Single(evt.Registrations);
        Assert.Equal(DefaultParticipantName, registration.ParticipantName);
        Assert.Equal(DefaultParticipantEmail, registration.ParticipantEmail);
    }

    [Fact]
    public void RegisterParticipant_WhenEventIsFull_ThrowsEventIsFullException()
    {
        // Arrange
        var evt = CreateValidEvent(maxParticipants: 1);
        evt.RegisterParticipant(DefaultParticipantName, DefaultParticipantEmail);

        // Act & Assert
        Assert.Throws<EventIsFullException>(() =>
            evt.RegisterParticipant(AlternateParticipantName, AlternateParticipantEmail));
    }

    [Fact]
    public void RegisterParticipant_WithDuplicateEmail_ThrowsDuplicateRegistrationException()
    {
        // Arrange
        var evt = CreateValidEvent();
        evt.RegisterParticipant(DefaultParticipantName, DefaultParticipantEmail);

        // Act & Assert
        Assert.Throws<DuplicateRegistrationException>(() =>
            evt.RegisterParticipant(DefaultParticipantName, DefaultParticipantEmail));
    }

    [Fact]
    public void RegisterParticipant_WithDuplicateEmailDifferentCase_ThrowsDuplicateRegistrationException()
    {
        // Arrange
        var evt = CreateValidEvent();
        evt.RegisterParticipant(DefaultParticipantName, DefaultParticipantEmail);

        // Act & Assert
        Assert.Throws<DuplicateRegistrationException>(() =>
            evt.RegisterParticipant(DefaultParticipantName, DefaultParticipantEmail.ToUpper()));
    }

    [Fact]
    public void Registrations_ReturnsReadOnlyList()
    {
        // Arrange
        var evt = CreateValidEvent();

        // Act
        var registrations = evt.Registrations;

        // Assert
        Assert.IsAssignableFrom<IReadOnlyCollection<Registration>>(registrations);
    }
}
