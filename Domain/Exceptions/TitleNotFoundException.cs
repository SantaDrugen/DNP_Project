namespace Domain.Exceptions;

public class TitleNotFoundException : Exception
{
    
    public TitleNotFoundException()
    {
    }

    public TitleNotFoundException(string message) : base(message)
    {
    }
    
}