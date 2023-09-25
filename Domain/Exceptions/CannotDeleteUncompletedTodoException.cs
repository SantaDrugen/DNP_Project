namespace Domain.Exceptions;

public class CannotDeleteUncompletedTodoException : Exception
{
    public CannotDeleteUncompletedTodoException()
    {
    }

    public CannotDeleteUncompletedTodoException(string message) : base(message)
    {
    }
}