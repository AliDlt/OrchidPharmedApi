using Microsoft.EntityFrameworkCore;
using OrchidPharmedApi.DataAccess.DataContext;
using OrchidPharmedApi.DataAccess.Repositories;
using OrchidPharmedApi.Entities;
using Xunit;

public class ProjectRepositoryTests
{
    private readonly AppDbContext _context;
    private readonly ProjectRepository _projectRepository;

    public ProjectRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "ProjectDb").Options;
        _context = new AppDbContext(options);
        _projectRepository = new ProjectRepository(_context);
    }

    [Fact]
    public async Task CreateProject_ShouldAddProjectToDatabase()
    {
        // Arrange
        var project = new ProjectEntity { Name = "New Project", Description = "Test Description" };

        // Act
        await _projectRepository.CreateProjectAsync(project);
        var result = _context.Projects.FirstOrDefault(p => p.Name == "New Project");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("New Project", result.Name);
    }

    [Fact]
    public async Task GetProjects_ShouldReturnAllProjects()
    {
        // Arrange
        _context.Projects.Add(new ProjectEntity { Name = "Project 1", Description = "Description 1" });
        _context.Projects.Add(new ProjectEntity { Name = "Project 2", Description = "Description 2" });
        await _context.SaveChangesAsync();

        // Act
        var projects = await _projectRepository.GetProjectsAsync();

        // Assert
        Assert.Equal(2, projects.Count());
    }

    [Fact]
    public async Task DeleteProject_ShouldRemoveProjectFromDatabase()
    {
        // Arrange
        var project = new ProjectEntity { Name = "Project to Delete", Description = "Description" };
        await _context.Projects.AddAsync(project);
        await _context.SaveChangesAsync();

        // Act
        await _projectRepository.DeleteProjectAsync(project);
        var result = _context.Projects.FirstOrDefault(p => p.Name == "Project to Delete");

        // Assert
        Assert.Null(result);
    }
}
