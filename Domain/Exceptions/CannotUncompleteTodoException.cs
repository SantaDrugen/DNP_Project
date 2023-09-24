namespace Domain.Exceptions;

public class CannotUncompleteTodoException : Exception
{
    public CannotUncompleteTodoException()
    {
    }

    public CannotUncompleteTodoException(string message) : base(message)
    {
    }
}