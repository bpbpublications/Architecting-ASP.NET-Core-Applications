using Core.Models;
namespace Infrastructure.Interfaces;
public interface ITaskPort
{
    IEnumerable<TaskModel> GetAllTasks();
    void AddTask(TaskModel task);
    void UpdateTask(TaskModel task);
    void DeleteTask(int taskId);
}
