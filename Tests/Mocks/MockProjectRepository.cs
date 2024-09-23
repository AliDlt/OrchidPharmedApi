using OrchidPharmedApi.Core.Interfaces;
using OrchidPharmedApi.Entities;

public class MockProjectRepository : IProjectRepository
{
    private readonly List<ProjectEntity> _projects = new();

    public Task<IEnumerable<ProjectEntity>> GetProjectsAsync()
    {
        return Task.FromResult(_projects.AsEnumerable());
    }

    public Task<ProjectEntity> GetProjectByIdAsync(int id)
    {
        var project = _projects.FirstOrDefault(p => p.Id == id);
        return Task.FromResult(project);
    }

    public Task<ProjectEntity> CreateProjectAsync(ProjectEntity project)
    {
        project.Id = _projects.Count + 1;
        _projects.Add(project);
        return Task.FromResult(project);
    }

    public Task DeleteProjectAsync(ProjectEntity project)
    {
        _projects.Remove(project);
        return Task.CompletedTask;
    }
}
