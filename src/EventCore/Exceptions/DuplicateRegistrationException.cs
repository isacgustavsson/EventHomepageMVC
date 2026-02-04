namespace EventCore.Exceptions;

public class DuplicateRegistrationException : Exception
{
    public DuplicateRegistrationException() : base("A registration with this email already exists for this event") { }
}

