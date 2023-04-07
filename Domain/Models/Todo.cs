namespace Domain.Models;

public class Todo
{
    
    //We have created a constructor,
    //which only takes two of the four properties as arguments.
    //The intention is that the Id should be set automatically by whatever
    //class persists the data, and you cannot create a Todo,
    //which is initially already completed, so we just default IsCompleted to false,
    //by not setting it.
    
    public int Id { get; set; }
    public User Owner { get; }
    public string Title { get; }
    public bool IsCompleted { get; set; }

    public Todo(User owner, string title)
    {
        Owner = owner;
        Title = title;
    }
}