using FluentValidation;
using TodoApi.Models.DTOs.ToDoItems;

namespace TodoApi.Validators.ToDoItem
{
    public class ToDoItemUpdateValidator : AbstractValidator<ToDoItemUpdateDto>
    {
        public ToDoItemUpdateValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MinimumLength(3).WithMessage("Title must be at least 3 characters long.")
                .MaximumLength(200).WithMessage("Title can't exceed 200 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(1000).WithMessage("Description can't exceed 1000 characters.");

            RuleFor(x => x.DueDate)
                .NotEmpty().WithMessage("Due Date is required.")
                .GreaterThan(DateTime.UtcNow).WithMessage("Due Date must be in the future.");
        }
    }
}
