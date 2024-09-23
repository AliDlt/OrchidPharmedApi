using OrchidPharmedApi.Entities;

namespace OrchidPharmedApi.Core.Interfaces
{
    public interface ITaskEntityService
    {
        Task<IEnumerable<TaskEntity>> GetTaskEntitysAsync(int projectId);
        Task<TaskEntity> CreateTaskEntityAsync(TaskEntity TaskEntity);
        Task UpdateTaskEntityStatusAsync(int TaskEntityId, TaskEntityStatus status);
        Task DeleteTaskEntityAsync(int id);
    }
}
