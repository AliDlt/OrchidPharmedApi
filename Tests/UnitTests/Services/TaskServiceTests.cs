using Moq;
using OrchidPharmedApi.Core.Interfaces;
using OrchidPharmedApi.Core.Services;
using OrchidPharmedApi.Entities;
using Xunit;

public class TaskServiceTests
{
    private readonly Mock<ITaskEntityRepository> _mockTaskEntityRepository;
    private readonly ITaskEntityService _TaskEntityService;

    public TaskServiceTests()
    {
        _mockTaskEntityRepository = new Mock<ITaskEntityRepository>();
        _TaskEntityService = new TaskEntityService(_mockTaskEntityRepository.Object);
    }

    [Fact]
    public async Task CreateTaskEntity_ShouldAddNewTaskEntity()
    {
        // Arrange
        var newTaskEntity = new TaskEntity { Name = "New TaskEntity", Description = "TaskEntity Description", DueDate = System.DateTime.Now.AddDays(2), Status = TaskEntityStatus.ToDo };
        _mockTaskEntityRepository.Setup(repo => repo.CreateTaskEntityAsync(It.IsAny<TaskEntity>()))
            .ReturnsAsync(newTaskEntity);

        // Act
        var result = await _TaskEntityService.CreateTaskEntityAsync(newTaskEntity);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("New TaskEntity", result.Name);
        Assert.Equal("TaskEntity Description", result.Description);
        Assert.Equal(TaskEntityStatus.ToDo, result.Status);
    }

    [Fact]
    public async Task GetTaskEntitys_ShouldReturnTaskEntityList()
    {
        // Arrange
        var TaskEntityList = new List<TaskEntity>
        {
            new TaskEntity { Id = 1, Name = "TaskEntity 1", Description = "Description 1", Status = TaskEntityStatus.ToDo },
            new TaskEntity { Id = 2, Name = "TaskEntity 2", Description = "Description 2", Status = TaskEntityStatus.InProgress }
        };
        _mockTaskEntityRepository.Setup(repo => repo.GetTaskEntitiesAsync(It.IsAny<int>()))
            .ReturnsAsync(TaskEntityList);

        // Act
        var result = await _TaskEntityService.GetTaskEntitysAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task UpdateTaskEntityStatus_ShouldChangeStatus()
    {
        // Arrange
        var TaskEntity = new TaskEntity { Id = 1, Name = "TaskEntity 1", Status = TaskEntityStatus.ToDo };
        _mockTaskEntityRepository.Setup(repo => repo.GetTaskEntityByIdAsync(1))
            .ReturnsAsync(TaskEntity);
        _mockTaskEntityRepository.Setup(repo => repo.UpdateTaskEntityStatusAsync(1, TaskEntityStatus.Done));

        // Act
        await _TaskEntityService.UpdateTaskEntityStatusAsync(1, TaskEntityStatus.Done);

        // Assert
        _mockTaskEntityRepository.Verify(repo => repo.UpdateTaskEntityStatusAsync(1, TaskEntityStatus.Done), Times.Once);
    }
}
