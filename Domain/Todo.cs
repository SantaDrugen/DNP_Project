namespace Domain;

public class Todo
{
    public int Id { get; set; }
    public User Owner { get; }
    public string Title { get; }
    public bool IsCompleted { get; }


    public Todo(User owner, string title)
    {
        Owner = owner;
        Title = title;
        //IsCompleted default false
    }
}