using Blog.App.Db.Entities;
using FluentValidation;

namespace Blog.App.Validations
{
    public class AuthorValidations: AbstractValidator<AuthorEntity>
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
