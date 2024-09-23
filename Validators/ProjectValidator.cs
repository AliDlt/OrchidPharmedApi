using FluentValidation;
using OrchidPharmedApi.Core.DTOs;

namespace OrchidPharmedApi.Validators
{
    public class ProjectValidator : AbstractValidator<ProjectDTO>
    {
        public ProjectValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Project name is required");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Project description is required");
        }
    }
}
