using OrchidPharmedApi.Core.Interfaces;
using OrchidPharmedApi.Entities;

public class MockTaskEntityRepository : ITaskEntityRepository
{
    private readonly List<TaskEntity> _taskEntities = new();

    public Task<IEnumerable<TaskEntity>> GetTaskEntitiesAsync(int projectId)
    {
        return Task.FromResult(_taskEntities.Where(t => t.ProjectId == projectId).AsEnumerable());
    }

    public Task<TaskEntity> GetTaskEntityByIdAsync(int id)
    {
        return Task.FromResult(_taskEntities.FirstOrDefault(t => t.Id == id));
    }

    public Task<TaskEntity> CreateTaskEntityAsync(TaskEntity taskEntity)
    {
        taskEntity.Id = _taskEntities.Count + 1;
        _taskEntities.Add(taskEntity);
        return Task.FromResult(taskEntity);
    }

    public Task UpdateTaskEntityStatusAsync(int taskEntityId, TaskEntityStatus status)
    {
        var taskEntity = _taskEntities.FirstOrDefault(t => t.Id == taskEntityId);
        if (taskEntity != null)
        {
            taskEntity.Status = status;
        }
        return Task.CompletedTask;
    }

    public Task DeleteTaskEntityAsync(TaskEntity taskEntity)
    {
        _taskEntities.Remove(taskEntity);
        return Task.CompletedTask;
    }
}
