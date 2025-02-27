namespace Application.Adapters;

using Application.Interfaces;
using Core.Models;
using Infrastructure.Interfaces;
using System.Collections.Generic;

public class TaskManagementAdapter : ITaskManagementPort
{
    private ITaskPort adapter;
    public TaskManagementAdapter(ITaskPort adapter) 
        => this.adapter = adapter;
    public IEnumerable<TaskModel> GetAllTasks() 
        => adapter.GetAllTasks();
    public void AddTask(TaskModel task) 
        => adapter.AddTask(task);
    public void UpdateTask(TaskModel task) 
        => adapter.UpdateTask(task);
    public void DeleteTask(int taskId) 
        => adapter.DeleteTask(taskId);
}