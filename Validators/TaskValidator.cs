using FluentValidation;
using OrchidPharmedApi.Core.DTOs;

namespace OrchidPharmedApi.Validators
{
    public class TaskEntityValidator : AbstractValidator<TaskEntityDTO>
    {
        public TaskEntityValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("TaskEntity name is required");
            RuleFor(x => x.Description).NotEmpty().WithMessage("TaskEntity description is required");
            RuleFor(x => x.DueDate).NotEmpty().WithMessage("Due date is required");
        }
    }
}
