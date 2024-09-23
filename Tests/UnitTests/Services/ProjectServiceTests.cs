using Moq;
using OrchidPharmedApi.Core.Interfaces;
using OrchidPharmedApi.Core.Services;
using OrchidPharmedApi.Entities;
using Xunit;

public class ProjectServiceTests
{
    private readonly Mock<IProjectRepository> _mockProjectRepository;
    private readonly IProjectService _projectService;

    public ProjectServiceTests()
    {
        _mockProjectRepository = new Mock<IProjectRepository>();
        _projectService = new ProjectService(_mockProjectRepository.Object);
    }

    [Fact]
    public async Task CreateProject_ShouldAddNewProject()
    {
        // Arrange
        var newProject = new ProjectEntity { Name = "New Project", Description = "Test Description" };
        _mockProjectRepository.Setup(repo => repo.CreateProjectAsync(It.IsAny<ProjectEntity>()))
            .ReturnsAsync(newProject);

        // Act
        var result = await _projectService.CreateProjectAsync(newProject);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("New Project", result.Name);
        Assert.Equal("Test Description", result.Description);
    }

    [Fact]
    public async Task GetProjects_ShouldReturnProjectList()
    {
        // Arrange
        var projectList = new List<ProjectEntity>
        {
            new ProjectEntity { Id = 1, Name = "Project 1", Description = "Description 1" },
            new ProjectEntity { Id = 2, Name = "Project 2", Description = "Description 2" }
        };
        _mockProjectRepository.Setup(repo => repo.GetProjectsAsync())
            .ReturnsAsync(projectList);

        // Act
        var result = await _projectService.GetProjectsAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task DeleteProject_ShouldCallRepositoryDelete()
    {
        // Arrange
        var project = new ProjectEntity { Id = 1, Name = "Project 1" };
        _mockProjectRepository.Setup(repo => repo.GetProjectByIdAsync(1))
            .ReturnsAsync(project);
        _mockProjectRepository.Setup(repo => repo.DeleteProjectAsync(project));

        // Act
        await _projectService.DeleteProjectAsync(1);

        // Assert
        _mockProjectRepository.Verify(repo => repo.DeleteProjectAsync(project), Times.Once);
    }
}
