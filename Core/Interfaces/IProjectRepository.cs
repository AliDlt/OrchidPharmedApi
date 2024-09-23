using OrchidPharmedApi.Entities;

namespace OrchidPharmedApi.Core.Interfaces
{
    public interface IProjectRepository
    {
        Task<IEnumerable<ProjectEntity>> GetProjectsAsync();
        Task<ProjectEntity> GetProjectByIdAsync(int id);
        Task<ProjectEntity> CreateProjectAsync(ProjectEntity project);
        Task DeleteProjectAsync(ProjectEntity project);
    }
}
