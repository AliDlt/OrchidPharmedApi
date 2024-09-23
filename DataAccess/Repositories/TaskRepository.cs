using Microsoft.EntityFrameworkCore;
using OrchidPharmedApi.Core.Interfaces;
using OrchidPharmedApi.DataAccess.DataContext;
using OrchidPharmedApi.Entities;

namespace OrchidPharmedApi.DataAccess.Repositories
{
    public class TaskEntityRepository : ITaskEntityRepository
    {
        private readonly AppDbContext _context;

        public TaskEntityRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskEntity>> GetTaskEntitiesAsync(int projectId)
        {
            return await _context.TaskEntitys.Where(t => t.ProjectId == projectId).ToListAsync();
        }

        public async Task<TaskEntity> GetTaskEntityByIdAsync(int id)
        {
            return await _context.TaskEntitys.FindAsync(id);
        }

        public async Task<TaskEntity> CreateTaskEntityAsync(TaskEntity TaskEntity)
        {
            _context.TaskEntitys.Add(TaskEntity);
            await _context.SaveChangesAsync();
            return TaskEntity;
        }

        public async Task UpdateTaskEntityStatusAsync(int TaskEntityId, TaskEntityStatus status)
        {
            var TaskEntity = await _context.TaskEntitys.FindAsync(TaskEntityId);
            if (TaskEntity != null)
            {
                TaskEntity.Status = status;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteTaskEntityAsync(TaskEntity TaskEntity)
        {
            _context.TaskEntitys.Remove(TaskEntity);
            await _context.SaveChangesAsync();
        }
    }
}
