using FluentValidation.TestHelper;
using OrchidPharmedApi.Core.DTOs;
using OrchidPharmedApi.Validators;
using Xunit;

public class TaskEntityValidatorTests
{
    private readonly TaskEntityValidator _validator;

    public TaskEntityValidatorTests()
    {
        _validator = new TaskEntityValidator();
    }

    [Fact]
    public void Should_Have_Error_When_TaskEntityName_Is_Empty()
    {
        var TaskEntity = new TaskEntityDTO { Name = "", Description = "TaskEntity Description", DueDate = DateTime.Now.AddDays(1) };
        var result = _validator.TestValidate(TaskEntity);
        result.ShouldHaveValidationErrorFor(t => t.Name);
    }

    [Fact]
    public void Should_Have_Error_When_DueDate_Is_Empty()
    {
        var TaskEntity = new TaskEntityDTO { Name = "TaskEntity Name", Description = "TaskEntity Description", DueDate = DateTime.MinValue };
        var result = _validator.TestValidate(TaskEntity);
        result.ShouldHaveValidationErrorFor(t => t.DueDate);
    }

    [Fact]
    public void Should_Not_Have_Error_When_TaskEntity_Is_Valid()
    {
        var TaskEntity = new TaskEntityDTO { Name = "Valid TaskEntity", Description = "Valid Description", DueDate = DateTime.Now.AddDays(2) };
        var result = _validator.TestValidate(TaskEntity);
        result.ShouldNotHaveValidationErrorFor(t => t.Name);
        result.ShouldNotHaveValidationErrorFor(t => t.Description);
        result.ShouldNotHaveValidationErrorFor(t => t.DueDate);
    }
}
