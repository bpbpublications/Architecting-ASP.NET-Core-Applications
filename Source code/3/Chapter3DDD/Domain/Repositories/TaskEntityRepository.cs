using Core.Entities;
using Core.Interfaces;

namespace Domain.Repositories;

public class TaskEntityRepository : ITaskEntityRepository
{
    private readonly List<TaskEntity> entities = new List<TaskEntity>();

    public TaskEntity? GetById(int id) 
        => entities.FirstOrDefault(e => e.Id == id);

    public IEnumerable<TaskEntity> GetAll() 
        => entities;

    public void Add(TaskEntity entity) 
        => entities.Add(entity);

    public void Update(TaskEntity entity)
    {
        var existingEntity = entities.FirstOrDefault(e => e.Id == entity.Id); 
        if (existingEntity is not null)
        {
            var index = entities.IndexOf(existingEntity);
            entities[index] = entity;
        }
    }

    public void Delete(TaskEntity entity) 
        => entities.Remove(entity);
}

