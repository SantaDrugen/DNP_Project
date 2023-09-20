namespace Domain.Exceptions;

public class UnavailableUsernameException : Exception
{
    public UnavailableUsernameException()
    {
    }

    public UnavailableUsernameException(string message) : base(message)
    {
        
    }
}