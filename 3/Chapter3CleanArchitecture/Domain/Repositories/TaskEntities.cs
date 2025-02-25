using Core.Entities;
using Core.Interfaces;

namespace Domain.Repositories;

public class TaskEntities : ITaskEntities
{
    List<TaskEntity> entities = [];

    public void AddEntity(TaskEntity entity)
        => entities.Add(entity);

    public IEnumerable<TaskEntity> GetEntities() => entities;

    public TaskEntity? GetEntityFromId(int id)
        => entities.FirstOrDefault(x => x.Id == id);

    public void RemoveEntity(TaskEntity entity)
    {
        if (entity is not null)
            entities.Remove(entity);
    }
}

