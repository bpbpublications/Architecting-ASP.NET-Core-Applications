namespace Core.Entities;
public class TaskEntity
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
}
