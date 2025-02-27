using Chapter4MinimalAPIWithMVC.Shared.Models;
using System.Net.Mime;
using System.Text.Json;
using System.Text;
namespace Chapter4MinimalAPIWithMVC.Client.Services;
public class TaskListService : ITaskListService
{
    public async Task<bool> Create(HttpClient httpClient, TaskEntity taskEntity)
    {
        var senderStatus = JsonSerializer.Serialize(taskEntity);
        var requestContent = new StringContent(senderStatus, Encoding.UTF8, MediaTypeNames.Application.Json);
        var response = await httpClient.PostAsync($"{Endpoints.TasksRootApiEndpointV1}{Endpoints.ActualVersion}{Endpoints.TasksApiEndpointV1}", requestContent);
        return response.IsSuccessStatusCode;
    }
    public async Task<List<TaskEntity>?> ReadAll(HttpClient httpClient)
    {
        var response = await httpClient.GetAsync($"{Endpoints.TasksRootApiEndpointV1}{Endpoints.ActualVersion}{Endpoints.TasksApiEndpointV1}");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var jsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var resp = JsonSerializer.Deserialize<List<TaskEntity>>(content, jsonSerializerOptions);
            if (resp is not null)
            {
                return resp;
            }
        }
        return null;
    }
    public async Task<TaskEntity?> ReadById(HttpClient httpClient, int id)
    {
        var response = await httpClient.GetAsync($"{Endpoints.TasksRootApiEndpointV1}{Endpoints.ActualVersion}{Endpoints.TasksApiEndpointV1}/{id}");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var jsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var resp = JsonSerializer.Deserialize<TaskEntity>(content, jsonSerializerOptions);
            if (resp is not null)
            {
                return resp;
            }
        }
        return null;
    }
    public async Task<bool> Delete(HttpClient httpClient, int id)
    {
        var response = await httpClient.DeleteAsync($"{Endpoints.TasksRootApiEndpointV1}{Endpoints.ActualVersion}{Endpoints.TasksApiEndpointV1}/{id}");
        return response.IsSuccessStatusCode;
    }
    public async Task<bool> Update(HttpClient httpClient, TaskEntity taskEntity)
    {
        var senderStatus = JsonSerializer.Serialize(taskEntity);
        var requestContent = new StringContent(senderStatus, Encoding.UTF8, MediaTypeNames.Application.Json);
        var response = await httpClient.PutAsync($"{Endpoints.TasksRootApiEndpointV1}{Endpoints.ActualVersion}{Endpoints.TasksApiEndpointV1}", requestContent);
        return response.IsSuccessStatusCode;
    }
}
