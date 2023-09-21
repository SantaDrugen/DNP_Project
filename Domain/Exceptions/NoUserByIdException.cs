namespace Domain.Exceptions;

public class NoUserByIdException : Exception
{
    
    public NoUserByIdException()
    {
    }

    public NoUserByIdException(string message) : base(message)
    {
    }
    
}