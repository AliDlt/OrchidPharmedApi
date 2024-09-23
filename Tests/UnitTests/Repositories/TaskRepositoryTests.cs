using Microsoft.EntityFrameworkCore;
using OrchidPharmedApi.DataAccess.DataContext;
using OrchidPharmedApi.DataAccess.Repositories;
using OrchidPharmedApi.Entities;
using Xunit;

public class TaskEntityRepositoryTests
{
    private readonly AppDbContext _context;
    private readonly TaskEntityRepository _TaskEntityRepository;

    public TaskEntityRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TaskEntityDb").Options;
        _context = new AppDbContext(options);
        _TaskEntityRepository = new TaskEntityRepository(_context);
    }

    [Fact]
    public async Task CreateTaskEntity_ShouldAddTaskEntityToDatabase()
    {
        // Arrange
        var TaskEntity = new TaskEntity { Name = "New TaskEntity", Description = "TaskEntity Description", DueDate = System.DateTime.Now, Status = TaskEntityStatus.ToDo };

        // Act
        await _TaskEntityRepository.CreateTaskEntityAsync(TaskEntity);
        var result = _context.TaskEntitys.FirstOrDefault(t => t.Name == "New TaskEntity");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("New TaskEntity", result.Name);
    }

    [Fact]
    public async Task GetTaskEntitys_ShouldReturnAllTaskEntitysForProject()
    {
        // Arrange
        var project = new ProjectEntity { Name = "Test Project" };
        await _context.Projects.AddAsync(project);
        await _context.TaskEntitys.AddAsync(new TaskEntity { Name = "TaskEntity 1", Description = "Description 1", ProjectId = project.Id });
        await _context.TaskEntitys.AddAsync(new TaskEntity { Name = "TaskEntity 2", Description = "Description 2", ProjectId = project.Id });
        await _context.SaveChangesAsync();

        // Act
        var TaskEntitys = await _TaskEntityRepository.GetTaskEntitiesAsync(project.Id);

        // Assert
        Assert.Equal(2, TaskEntitys.Count());
    }

    [Fact]
    public async Task UpdateTaskEntityStatus_ShouldChangeTaskEntityStatus()
    {
        // Arrange
        var TaskEntity = new TaskEntity { Name = "Test TaskEntity", Description = "TaskEntity Description", Status = TaskEntityStatus.ToDo };
        await _context.TaskEntitys.AddAsync(TaskEntity);
        await _context.SaveChangesAsync();

        // Act
        await _TaskEntityRepository.UpdateTaskEntityStatusAsync(TaskEntity.Id, TaskEntityStatus.Done);
        var result = await _context.TaskEntitys.FindAsync(TaskEntity.Id);

        // Assert
        Assert.Equal(TaskEntityStatus.Done, result.Status);
    }
}
