using Blog.App.Db.Entities;
using FluentValidation;

namespace Blog.App.Validations
{
    public class BlogValidations: AbstractValidator<BlogPostEntity>
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
