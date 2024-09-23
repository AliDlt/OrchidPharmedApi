using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using OrchidPharmedApi.Core.DTOs;
using System.Net;
using System.Text;
using Xunit;

public class ProjectControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ProjectControllerTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetProjects_ShouldReturnOkStatus()
    {
        // Act
        var response = await _client.GetAsync("/api/projects");

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        Assert.NotEmpty(content);
    }

    [Fact]
    public async Task CreateProject_ShouldReturnCreatedStatus()
    {
        // Arrange
        var project = new ProjectDTO { Name = "Test Project", Description = "Test Description" };
        var content = new StringContent(JsonConvert.SerializeObject(project), Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PostAsync("/api/projects", content);

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }
}
