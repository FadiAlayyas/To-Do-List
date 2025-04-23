using FluentValidation;
using TodoApi.Models.DTOs.ToDoLists;

namespace TodoApi.Validators.ToDoList
{
    public class ToDoListCreateValidator : AbstractValidator<ToDoListCreateDto>
    {
        public ToDoListCreateValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MinimumLength(3).WithMessage("Title must be at least 3 characters long.")
                .MaximumLength(150).WithMessage("Title cannot be longer than 150 characters.");

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User ID is required.");

            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("Category ID is required.");
        }
    }
}
