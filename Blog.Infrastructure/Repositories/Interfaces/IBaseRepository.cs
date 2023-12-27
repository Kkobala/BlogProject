using Blog.Domain.Entities;
using Blog.Common.Models;

namespace Blog.Infrastructure.Repositories.Interfaces
{
    public interface IBaseRepository
    {
        Task AddBlogPost(BlogPostEntity entity);
        Task AddAuthor(AuthorEntity entity);
        void Update(BlogPostEntity entity);
        void Delete(BlogPostEntity entity);
        Task<AuthorEntity> GetAuthorById(int id);
        Task<BlogPostEntity> GetBlogById(int id);
        Task<List<BlogDTO>> GetAllBlogsAsync();
        Task<List<BlogDTO>> SortBlogsAsync(string sortby, string filterByAuthor);
        Task SaveChanges();
    }
}
