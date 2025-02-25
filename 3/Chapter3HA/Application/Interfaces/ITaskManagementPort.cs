using Core.Models;

namespace Application.Interfaces;
public interface ITaskManagementPort
{
    IEnumerable<TaskModel> GetAllTasks();
    void AddTask(TaskModel task);
    void UpdateTask(TaskModel task);
    void DeleteTask(int taskId);
}