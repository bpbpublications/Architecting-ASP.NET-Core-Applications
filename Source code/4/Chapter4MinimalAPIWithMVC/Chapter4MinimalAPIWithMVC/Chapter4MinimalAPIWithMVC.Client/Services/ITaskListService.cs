using Chapter4MinimalAPIWithMVC.Shared.Models;

namespace Chapter4MinimalAPIWithMVC.Client.Services;

public interface ITaskListService
{
    Task<bool> Create(HttpClient httpClient, TaskEntity taskEntity);
    Task<List<TaskEntity>?> ReadAll(HttpClient httpClient);
    Task<TaskEntity?> ReadById(HttpClient httpClient, int id);
    Task<bool> Update(HttpClient httpClient, TaskEntity taskEntity);
    Task<bool> Delete(HttpClient httpClient, int id);
}
