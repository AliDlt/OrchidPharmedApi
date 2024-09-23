using FluentValidation.TestHelper;
using OrchidPharmedApi.Core.DTOs;
using OrchidPharmedApi.Validators;
using Xunit;

public class ProjectValidatorTests
{
    private readonly ProjectValidator _validator;

    public ProjectValidatorTests()
    {
        _validator = new ProjectValidator();
    }

    [Fact]
    public void Should_Have_Error_When_ProjectName_Is_Empty()
    {
        var project = new ProjectDTO { Name = "", Description = "Test Description" };
        var result = _validator.TestValidate(project);
        result.ShouldHaveValidationErrorFor(p => p.Name);
    }

    [Fact]
    public void Should_Have_Error_When_Description_Is_Empty()
    {
        var project = new ProjectDTO { Name = "Test Project", Description = "" };
        var result = _validator.TestValidate(project);
        result.ShouldHaveValidationErrorFor(p => p.Description);
    }

    [Fact]
    public void Should_Not_Have_Error_When_Project_Is_Valid()
    {
        var project = new ProjectDTO { Name = "Valid Project", Description = "Valid Description" };
        var result = _validator.TestValidate(project);
        result.ShouldNotHaveValidationErrorFor(p => p.Name);
        result.ShouldNotHaveValidationErrorFor(p => p.Description);
    }
}
