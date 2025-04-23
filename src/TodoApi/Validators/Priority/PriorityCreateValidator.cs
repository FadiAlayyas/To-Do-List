using FluentValidation;
using TodoApi.Models.DTOs.Priorities;

namespace TodoApi.Validators.Priority
{
    public class PriorityCreateValidator : AbstractValidator<PriorityCreateDto>
    {
        public PriorityCreateValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name cannot be longer than 100 characters.");
        }
    }
} 