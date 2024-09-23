using Microsoft.AspNetCore.Mvc;
using OrchidPharmedApi.Core.DTOs;
using OrchidPharmedApi.Core.Interfaces;
using OrchidPharmedApi.Entities;

namespace OrchidPharmedApi.WebAPI.Controllers
{
    [Route("api/projects/{projectId}/taskentities")]
    [ApiController]
    public class TaskEntitiesController : ControllerBase
    {
        private readonly ITaskEntityService _taskEntityService;

        public TaskEntitiesController(ITaskEntityService taskEntityService)
        {
            _taskEntityService = taskEntityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTaskEntities(int projectId)
        {
            var taskEntities = await _taskEntityService.GetTaskEntitysAsync(projectId);
            return Ok(taskEntities);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTaskEntity(int projectId, [FromBody] TaskEntityDTO taskEntityDTO)
        {
            var taskEntity = new TaskEntity
            {
                Name = taskEntityDTO.Name,
                Description = taskEntityDTO.Description,
                DueDate = taskEntityDTO.DueDate,
                ProjectId = projectId,
                Status = TaskEntityStatus.ToDo
            };

            var createdTaskEntity = await _taskEntityService.CreateTaskEntityAsync(taskEntity);
            return CreatedAtAction(nameof(GetTaskEntities), new { id = createdTaskEntity.Id }, createdTaskEntity);
        }

        [HttpPatch("{taskEntityId}/status")]
        public async Task<IActionResult> UpdateTaskEntityStatus(int projectId, int taskEntityId, [FromBody] TaskEntityStatus status)
        {
            await _taskEntityService.UpdateTaskEntityStatusAsync(taskEntityId, status);
            return NoContent();
        }

        [HttpDelete("{taskEntityId}")]
        public async Task<IActionResult> DeleteTaskEntity(int projectId, int taskEntityId)
        {
            await _taskEntityService.DeleteTaskEntityAsync(taskEntityId);
            return NoContent();
        }
    }
}
