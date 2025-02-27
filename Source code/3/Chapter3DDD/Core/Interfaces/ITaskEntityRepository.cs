using Core.Entities;

namespace Core.Interfaces;
public interface ITaskEntityRepository
{
    TaskEntity? GetById(int id);
    IEnumerable<TaskEntity> GetAll();
    void Add(TaskEntity entity);
    void Update(TaskEntity entity);
    void Delete(TaskEntity entity);
}

