namespace Domain.Exceptions;

public class NoTodoByIdException : Exception
{
    
    public NoTodoByIdException()
    {
    }

    public NoTodoByIdException(string message) : base(message)
    {
    }
    
}