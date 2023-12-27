using Blog.Common.Models;
using FluentValidation;

namespace Blog.Common.Validations
{
    public class BlogValidations : AbstractValidator<BlogDTO>
    {
        public BlogValidations()
        {
            RuleFor(blog => blog.Title).MinimumLength(20).MaximumLength(255);
            RuleFor(blog => blog.AuthorId).NotNull();
            RuleFor(blog => blog.Content).NotEmpty();
            RuleFor(blog => blog.Conclusion).MaximumLength(500);
        }
    }
}
