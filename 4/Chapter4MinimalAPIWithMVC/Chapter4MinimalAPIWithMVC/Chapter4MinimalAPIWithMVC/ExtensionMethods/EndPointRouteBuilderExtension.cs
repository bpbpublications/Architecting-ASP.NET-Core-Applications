using Chapter4MinimalAPIWithMVC.Repositories;
using Chapter4MinimalAPIWithMVC.Shared.Models;
using Microsoft.AspNetCore.Mvc;
namespace Chapter4MinimalAPIWithMVC.ExtensionMethods;
public static class EndPointRouteBuilderExtension
{
    public static IEndpointRouteBuilder UseTaskListEndpoints(
            this IEndpointRouteBuilder app)
    {
        _ = app.MapGroup(Endpoints.TasksRootApiEndpointV1)
               .MapApi();
        return app;
    }
    private static RouteGroupBuilder MapApi(this RouteGroupBuilder group)
    {
        _ = group.MapGet($"{Endpoints.ActualVersion}/tasks"
            , GetAllTasksApi);
        _ = group.MapGet($"{Endpoints.ActualVersion}/tasks/{{id}}"
            , GetTaskByIdApi);
        _ = group.MapPost($"{Endpoints.ActualVersion}/tasks"
            , PostCreateTaskApi);
        _ = group.MapPut($"{Endpoints.ActualVersion}/tasks"
            , PutTaskApi);
        _ = group.MapDelete($"{Endpoints.ActualVersion}/tasks/{{id}}"
            , DeleteTaskApi);
        return group;
    }
    // Retrieve and return list of tasks
    private static IResult GetAllTasksApi(ITaskRepository repo)
        => Results.Ok(repo?.ReadAll());
    // Retrieve and return a task with specified id
    private static IResult GetTaskByIdApi(int id, ITaskRepository repo)
        => Results.Ok(repo?.ReadTaskById(id));
    // Create new task
    private static IResult PostCreateTaskApi([FromBody] TaskEntity newTask, ITaskRepository repo)
        => Results.Ok(repo?.CreateNewTask(newTask));
    // Update existing task with specified id.
    private static IResult PutTaskApi([FromBody] TaskEntity newTask, ITaskRepository repo)
        => Results.Ok(repo?.UpdateTask(newTask));
    // Delete an existing task from his id.
    private static IResult DeleteTaskApi(int id, ITaskRepository repo)
        => Results.Ok(repo?.DeleteTaskById(id));
}
