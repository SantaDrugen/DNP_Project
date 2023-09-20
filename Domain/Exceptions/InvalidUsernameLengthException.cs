namespace Domain.Exceptions;

public class InvalidUsernameLengthException : Exception
{
    public InvalidUsernameLengthException()
    {
    }

    public InvalidUsernameLengthException(string message) : base(message)
    {
    }
}