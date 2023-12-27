using Blog.Common.Models;
using FluentValidation;

namespace Blog.Common.Validations
{
    public class AuthorValidations : AbstractValidator<AuthorDTO>
    {
        public AuthorValidations()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.BIO).MinimumLength(10).MaximumLength(250);
            RuleFor(x => x.BirthDate).NotNull();
        }
    }
}
