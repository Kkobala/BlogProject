using Blog.App.BlogRequests;
using Blog.App.Db;
using Blog.App.Db.Entities;
using Blog.App.Models;
using Blog.App.Services.Interfaces;
using Blog.App.UnitOfWork.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Blog.App.Services.Implementation
{
    public class BlogServices : IBlogService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly BlogDbContext _db;
        private readonly IValidator<AuthorEntity> _authorRules;
        private readonly IValidator<BlogPostEntity> _blogRules;

        public BlogServices(
            IUnitOfWork unitOfWork,
            BlogDbContext db,
            IValidator<BlogPostEntity> blogRules,
            IValidator<AuthorEntity> authRules)
        {
            _unitOfWork = unitOfWork;
            _db = db;
            _blogRules = blogRules;
            _authorRules = authRules;
        }

        public async Task<AuthorDTO> RegisterAuthor(RegisterAuthorRequest request)
        {
            var authorEntity = new AuthorEntity
            {
                Name = request.Name,
                LastName = request.LastName,
                BIO = request.BIO,
                BirthDate = request.BirthDate
            };

            var results = await _authorRules.ValidateAsync(authorEntity);

            if (!results.IsValid)
            {
                foreach (var failure in results.Errors)
                {
                    Console.WriteLine("Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage);
                }
            }

            await _unitOfWork.BaseRepository.AddAuthor(authorEntity);
            await _unitOfWork.BaseRepository.SaveChanges();

            var author = new AuthorDTO
            {
                Name = request.Name,
                LastName = request.LastName,
                BIO = request.BIO,
                BirthDate = request.BirthDate
            };

            return author;
        }

        public async Task<BlogDTO> CreateBlogAsync(CreateBlogRequest request)
        {
            var author = await _db.Authors.FirstOrDefaultAsync(x => x.AuthorId == request.AuthorId)
               ?? throw new Exception($"Account with {request.AuthorId} cannot be found");

            var blogEntity = new BlogPostEntity
            {
                AuthorId = request.AuthorId,
                Title = request.Title,
                Content = request.Content,
                ImageUrl = request.ImageUrl,
                Quotes = request.Quotes,
                Conclusion = request.Conclusion,
                AuthorInformation = author
            };

            var results = await _blogRules.ValidateAsync(blogEntity);

            if (!results.IsValid)
            {
                foreach (var failure in results.Errors)
                {
                    Console.WriteLine("Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage);
                }
            }

            await _unitOfWork.BaseRepository.AddBlogPost(blogEntity);
            await _unitOfWork.BaseRepository.SaveChanges();

            var blog = new BlogDTO
            {
                AuthorId = blogEntity.AuthorId,
                Title = blogEntity.Title,
                Content = blogEntity.Content,
                ImageUrl = blogEntity.ImageUrl,
                Quotes = blogEntity.Quotes,
                Conclusion = blogEntity.Conclusion
            };

            return blog;
        }

        public async Task<BlogDTO> UpdateBlogAsync(UpdateBlogRequest request)
        {
            var blogEntity = await _unitOfWork.BaseRepository.GetBlogById(request.Id);

            if (blogEntity == null)
                throw new ArgumentException("Blog couldn't be found");

            blogEntity.Title = request.Title;
            blogEntity.Content = request.Content;
            blogEntity.ImageUrl = request.ImageUrl;

            _unitOfWork.BaseRepository.Update(blogEntity);
            await _unitOfWork.BaseRepository.SaveChanges();

            var blog = new BlogDTO
            {
                Title = request.Title,
                Content = request.Content,
                ImageUrl = request.ImageUrl
            };

            return blog;
        }

        public async Task RemoveBlog(RemoveBlogRequest request)
        {
            var blog = await _unitOfWork.BaseRepository.GetBlogById(request.Id);

            if (blog == null)
                throw new ArgumentException($"Blog with {request.Id} ID cannot be found");

            _unitOfWork.BaseRepository.Delete(blog);
            await _unitOfWork.BaseRepository.SaveChanges();
        }
    }
}
