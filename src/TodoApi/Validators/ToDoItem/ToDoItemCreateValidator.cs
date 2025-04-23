using FluentValidation;
using TodoApi.Models.DTOs.ToDoItems;

namespace TodoApi.Validators.ToDoItem
{
    public class ToDoItemCreateValidator : AbstractValidator<ToDoItemCreateDto>
    {
        public ToDoItemCreateValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MinimumLength(3).WithMessage("Title must be at least {MinLength} characters.")
                .MaximumLength(200).WithMessage("Title cannot exceed {MaxLength} characters.")
                .Matches(@"^[\w\s.,!?-]+$").WithMessage("Title contains invalid characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MinimumLength(10).WithMessage("Description must be at least {MinLength} characters.")
                .MaximumLength(1000).WithMessage("Description cannot exceed {MaxLength} characters.");

            RuleFor(x => x.DueDate)
                .NotEmpty().WithMessage("Due date is required.")
                .GreaterThan(DateTime.UtcNow).WithMessage("Due date must be in the future.")
                .LessThan(DateTime.UtcNow.AddYears(1)).WithMessage("Due date cannot be more than 1 year in the future.");

            RuleFor(x => x.ToDoListId)
                .NotEmpty().WithMessage("ToDo List is required.")
                .NotEqual(Guid.Empty).WithMessage("Invalid ToDo List selection.");

            RuleFor(x => x.PriorityId)
                .NotEmpty().WithMessage("Priority is required.")
                .NotEqual(Guid.Empty).WithMessage("Invalid Priority selection.");

            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("Category is required.")
                .NotEqual(Guid.Empty).WithMessage("Invalid Category selection.");

            RuleFor(x => x.IsCompleted)
                .Equal(false).WithMessage("Cannot create an already completed item.");
        }
    }
}