using OrchidPharmedApi.Core.Interfaces;
using OrchidPharmedApi.Entities;

namespace OrchidPharmedApi.Core.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<IEnumerable<ProjectEntity>> GetProjectsAsync()
        {
            return await _projectRepository.GetProjectsAsync();
        }

        public async Task<ProjectEntity> CreateProjectAsync(ProjectEntity project)
        {
            return await _projectRepository.CreateProjectAsync(project);
        }

        public async Task DeleteProjectAsync(int id)
        {
            var project = await _projectRepository.GetProjectByIdAsync(id);
            if (project != null)
            {
                await _projectRepository.DeleteProjectAsync(project);
            }
        }
    }
}
