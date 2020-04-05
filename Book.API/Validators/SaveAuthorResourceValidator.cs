using Book.API.Resources;
using FluentValidation;

namespace Book.API.Validators
{
    public class SaveAuthorResourceValidator : AbstractValidator<SaveAuthorResource>
    {
        public SaveAuthorResourceValidator()
        {
            RuleFor(resource => resource.Name)
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}