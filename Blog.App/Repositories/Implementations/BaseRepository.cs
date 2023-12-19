using Azure.Core;
using Blog.App.Db;
using Blog.App.Db.Entities;
using Blog.App.Models;
using Blog.App.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Blog.App.Repositories.Implementations
{
    public class BaseRepository: IBaseRepository
    {
        private readonly BlogDbContext _db;

        public BaseRepository(BlogDbContext db)
        {
            _db = db;
        }

        public async Task AddBlogPost(BlogPostEntity entity)
        {
            await _db.Blogs.AddAsync(entity);
        }

        public async Task AddAuthor(AuthorEntity entity)
        {
            await _db.Authors.AddAsync(entity);
        }

        public void Update(BlogPostEntity entity)
        {
            _db.Blogs.Update(entity);
        }

        public void Delete(BlogPostEntity entity)
        {
            _db.Blogs.Remove(entity);
        }

        public async Task<AuthorEntity> GetAuthorById(int id)
        {
            var author = await _db.Authors.FirstOrDefaultAsync(x => x.AuthorId == id)
                ?? throw new Exception($"Account with {id} cannot be found"); 

            return author;
        }

        public async Task<BlogPostEntity> GetBlogById(int id)
        {
            var blog = await _db.Blogs
            .Include(b => b.AuthorInformation)
            .FirstOrDefaultAsync(b => b.PostId == id);

            if (blog == null)
            {
                throw new ArgumentException($"Couldn't find blog with {id} ID");
            }

            return blog;
        }

        public async Task<List<BlogDTO>> GetAllBlogsAsync()
        {
            var blogs = await _db.Blogs
            .Include(b => b.AuthorInformation)
            .ToListAsync();

            return blogs.Select(blogEntity => new BlogDTO
            {
                AuthorId = blogEntity.AuthorId,
                Title = blogEntity.Title,
                Content = blogEntity.Content,
                ImageUrl = blogEntity.ImageUrl,
                Quotes = blogEntity.Quotes,
                Conclusion = blogEntity.Conclusion,
                AuthorInformation = blogEntity.AuthorInformation
            }).ToList();
        }

        public async Task SaveChanges()
        {
            await _db.SaveChangesAsync();
        }
    }
}
