namespace ToDoApp.Models;

public class Tasks
{
  
    public Guid Id { get; set; }

    
    public Guid OwnerId { get; set; }

   
    public string TaskName { get; set; }
    
    public string TaskDesc { get; set; }

    public bool IsDone { get; set; } = false;
}