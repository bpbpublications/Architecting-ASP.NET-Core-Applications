using Chapter4MinimalAPIWithMVC.Shared.Models;
namespace Chapter4MinimalAPIWithMVC.Repositories;
public class TaskRepository : ITaskRepository
{
    public List<TaskEntity> SavedEntities { get; private set; } = new List<TaskEntity>();
    public bool CreateNewTask(TaskEntity taskEntity)
    {
        taskEntity.Id = SavedEntities.Count + 1;
        SavedEntities.Add(taskEntity);
        return true;
    }
    public bool DeleteTaskById(int id)
    {
        var tsk = SavedEntities.Where(x => x.Id == id).ToList();
        if (tsk.Count > 0)
        {
            SavedEntities.Remove(tsk.First());
            return true;
        }
        return false;
    }
    public List<TaskEntity> ReadAll() 
        => SavedEntities;
    public TaskEntity? ReadTaskById(int id)
        => SavedEntities.Where(x => x.Id == id).FirstOrDefault();
    public bool UpdateTask(TaskEntity taskEntity)
    {
        var existingEntity = SavedEntities.FirstOrDefault(e => e.Id == taskEntity.Id);
        if (existingEntity is not null)
        {
            var index = SavedEntities.IndexOf(existingEntity);
            SavedEntities[index] = taskEntity;
            return true;
        }
        return false;
    }
}
