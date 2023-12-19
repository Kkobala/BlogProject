using Blog.App.Db.Entities;
using Blog.App.Models;

namespace Blog.App.Repositories.Interfaces
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
        Task SaveChanges();
    }
}
