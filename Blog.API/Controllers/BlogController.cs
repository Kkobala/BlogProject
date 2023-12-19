using Blog.App.BlogRequests;
using Blog.App.Services.Interfaces;
using Blog.App.UnitOfWork.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _blogService;
        private readonly IUnitOfWork _unitOfWork;

        public BlogController(
            IBlogService blogService,
            IUnitOfWork unitOfWork)
        {
            _blogService = blogService;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("register-author")]
        public async Task<IActionResult> RegisterAuthor(RegisterAuthorRequest request)
        {
            var author = await _blogService.RegisterAuthor(request);
            return Ok(author);
        }

        [HttpPost("create-blog-post")]
        public async Task<IActionResult> CreateBlog(CreateBlogRequest request)
        {
            var blog = await _blogService.CreateBlogAsync(request);
            return Ok(blog);
        }

        [HttpPut("update-blog-post")]
        public async Task<IActionResult> UpdateBlog(UpdateBlogRequest request)
        {
            var blog = await _blogService.UpdateBlogAsync(request);
            return Ok(blog);
        }

        [HttpDelete("delete-blog-post")]
        public async Task<IActionResult> DeleteBlog(RemoveBlogRequest request)
        {
            await _blogService.RemoveBlog(request);
            return Ok("Blog deleted successfully");
        }

        [HttpGet("get-all-blogs")]
        public async Task<IActionResult> GetAllBlogs()
        {
           return Ok(await _unitOfWork.BaseRepository.GetAllBlogsAsync());
        }

        [HttpGet("get-concrete-blog-by-id")]
        public async Task<IActionResult> GetConcreteBlogById(int id)
        {
           return Ok(await _unitOfWork.BaseRepository.GetBlogById(id));
        }
    }
}
