using Book.API.Resources;
using FluentValidation;

namespace Book.API.Validators
{
    public class SaveBookResourceValidator : AbstractValidator<SaveBookResource>
    {
        public SaveBookResourceValidator()
        {
            RuleFor(resource => resource.Name)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(resource => resource.AuthorId)
                .NotEmpty()
                .WithMessage("Author Id must not be equal to zero.");
        }
    }
}