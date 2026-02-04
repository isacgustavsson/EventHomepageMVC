namespace EventCore.Exceptions;

public class EventIsFullException : Exception
{
    public EventIsFullException() : base("Cannot add registration: event is full") { }
}

