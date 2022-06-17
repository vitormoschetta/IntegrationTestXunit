namespace IntegrationTest.Models;
public class TodoItem
{
    public TodoItem()
    {

    }

    public TodoItem(string title)
    {
        Title = title;        
    }

    public TodoItem(string title, bool done)
    {
        Title = title;
        Done = done;
    }

    public Guid Id { get; set; }
    public string Title { get; set; }
    public bool Done { get; set; }
}