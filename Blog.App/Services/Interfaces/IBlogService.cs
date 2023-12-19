using Blog.App.BlogRequests;
using Blog.App.Models;

namespace Blog.App.Services.Interfaces
{
    public interface IBlogService
    {
        Task<AuthorDTO> RegisterAuthor(RegisterAuthorRequest request);
        Task<BlogDTO> CreateBlogAsync(CreateBlogRequest request);
        Task<BlogDTO> UpdateBlogAsync(UpdateBlogRequest request);
        Task RemoveBlog(RemoveBlogRequest request);
    }
}
