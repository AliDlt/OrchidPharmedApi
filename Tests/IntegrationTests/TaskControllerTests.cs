using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using OrchidPharmedApi.Core.DTOs;
using OrchidPharmedApi.Entities;
using System.Net;
using System.Text;
using Xunit;

public class TaskControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public TaskControllerTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetTaskEntities_ShouldReturnOkStatus()
    {
        // Act
        var response = await _client.GetAsync("/api/projects/1/taskentities");

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        Assert.NotEmpty(content);
    }

    [Fact]
    public async Task CreateTaskEntity_ShouldReturnCreatedStatus()
    {
        // Arrange
        var taskEntity = new TaskEntityDTO { Name = "Test TaskEntity", Description = "TaskEntity Description", DueDate = System.DateTime.Now.AddDays(2), Status = TaskEntityStatus.ToDo };
        var content = new StringContent(JsonConvert.SerializeObject(taskEntity), Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PostAsync("/api/projects/1/taskentities", content);

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }
}
