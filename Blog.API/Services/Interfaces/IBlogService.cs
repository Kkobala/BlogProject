using Blog.API.BlogRequests;
using Blog.Common.Models;

namespace Blog.API.Services.Interfaces
{
    public interface IBlogService
    {
        Task<AuthorDTO> RegisterAuthor(RegisterAuthorRequest request);
        Task<BlogDTO> CreateBlogAsync(CreateBlogRequest request);
        Task<BlogDTO> UpdateBlogAsync(UpdateBlogRequest request);
        Task RemoveBlog(RemoveBlogRequest request);
    }
}
