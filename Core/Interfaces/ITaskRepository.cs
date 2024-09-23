using OrchidPharmedApi.Entities;

namespace OrchidPharmedApi.Core.Interfaces
{
    public interface ITaskEntityRepository
    {
        Task<IEnumerable<TaskEntity>> GetTaskEntitiesAsync(int projectId);
        Task<TaskEntity> GetTaskEntityByIdAsync(int id);
        Task<TaskEntity> CreateTaskEntityAsync(TaskEntity TaskEntity);
        Task UpdateTaskEntityStatusAsync(int TaskEntityId, TaskEntityStatus status);
        Task DeleteTaskEntityAsync(TaskEntity TaskEntity);
    }
}
