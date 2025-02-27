using Core.Entities;
using System.Collections.Generic;

namespace Core.Interfaces;
public interface ITaskEntities
{
    IEnumerable<TaskEntity> GetEntities();

    void AddEntity(TaskEntity entity);

    TaskEntity? GetEntityFromId(int id);

    void RemoveEntity(TaskEntity entity);
}

