using OrchidPharmedApi.Entities;

namespace OrchidPharmedApi.Core.Interfaces
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectEntity>> GetProjectsAsync();
        Task<ProjectEntity> CreateProjectAsync(ProjectEntity project);
        Task DeleteProjectAsync(int id);
    }
}
