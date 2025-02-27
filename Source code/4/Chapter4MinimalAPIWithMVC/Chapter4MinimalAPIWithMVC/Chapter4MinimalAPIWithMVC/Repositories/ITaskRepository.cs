using Chapter4MinimalAPIWithMVC.Shared.Models;
using System.Threading.Tasks;

namespace Chapter4MinimalAPIWithMVC.Repositories;

public interface ITaskRepository
{
    List<TaskEntity> ReadAll();
    bool CreateNewTask(TaskEntity taskEntity);
    bool DeleteTaskById(int id);
    bool UpdateTask(TaskEntity taskEntity);
    TaskEntity? ReadTaskById(int id);
    
}
