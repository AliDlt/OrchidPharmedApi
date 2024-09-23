using OrchidPharmedApi.Core.Interfaces;
using OrchidPharmedApi.Entities;

namespace OrchidPharmedApi.Core.Services
{
    public class TaskEntityService : ITaskEntityService
    {
        private readonly ITaskEntityRepository _TaskEntityRepository;

        public TaskEntityService(ITaskEntityRepository TaskEntityRepository)
        {
            _TaskEntityRepository = TaskEntityRepository;
        }

        public async Task<IEnumerable<TaskEntity>> GetTaskEntitysAsync(int projectId)
        {
            return await _TaskEntityRepository.GetTaskEntitiesAsync(projectId);
        }

        public async Task<TaskEntity> CreateTaskEntityAsync(TaskEntity TaskEntity)
        {
            return await _TaskEntityRepository.CreateTaskEntityAsync(TaskEntity);
        }

        public async Task UpdateTaskEntityStatusAsync(int TaskEntityId, TaskEntityStatus status)
        {
            await _TaskEntityRepository.UpdateTaskEntityStatusAsync(TaskEntityId, status);
        }

        public async Task DeleteTaskEntityAsync(int id)
        {
            var TaskEntity = await _TaskEntityRepository.GetTaskEntityByIdAsync(id);
            if (TaskEntity != null)
            {
                await _TaskEntityRepository.DeleteTaskEntityAsync(TaskEntity);
            }
        }
    }
}
