using FluentValidation;
using TodoApi.Models.DTOs.Categories;

namespace TodoApi.Validators.Category
{
    public class CategoryCreateValidator : AbstractValidator<CategoryCreateDto>
    {
        public CategoryCreateValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name cannot be longer than 100 characters.");
        }
    }
}
