using Blog.API.BlogRequests;
using Blog.Infrastructure.Repositories.Interfaces;
using Blog.API.Services.Interfaces;
using Blog.Infrastructure.UnitOfWork.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _blogService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBaseRepository _repo;

        public BlogController(
            IBlogService blogService,
            IUnitOfWork unitOfWork,
            IBaseRepository repo)
        {
            _blogService = blogService;
            _unitOfWork = unitOfWork;
            _repo = repo;
        }

        [Authorize(Policy = "ApiUser", AuthenticationSchemes = "Bearer")]
        [HttpPost("register-author")]
        public async Task<IActionResult> RegisterAuthor(RegisterAuthorRequest request)
        {
            var author = await _blogService.RegisterAuthor(request);
            return Ok(author);
        }

        [Authorize(Policy = "ApiUser", AuthenticationSchemes = "Bearer")]
        [HttpPost("create-blog-post")]
        public async Task<IActionResult> CreateBlog(CreateBlogRequest request)
        {
            var blog = await _blogService.CreateBlogAsync(request);
            return Ok(blog);
        }

        [Authorize(Policy = "ApiUser", AuthenticationSchemes = "Bearer")]
        [HttpPut("update-blog-post")]
        public async Task<IActionResult> UpdateBlog(UpdateBlogRequest request)
        {
            var blog = await _blogService.UpdateBlogAsync(request);
            return Ok(blog);
        }

        [Authorize(Policy = "ApiUser", AuthenticationSchemes = "Bearer")]
        [HttpDelete("delete-blog-post")]
        public async Task<IActionResult> DeleteBlog(RemoveBlogRequest request)
        {
            await _blogService.RemoveBlog(request);
            return Ok("Blog deleted successfully");
        }

        [Authorize(Policy = "ApiUser", AuthenticationSchemes = "Bearer")]
        [HttpGet("get-all-blogs")]
        public async Task<IActionResult> GetAllBlogs()
        {
           return Ok(await _unitOfWork.BaseRepository.GetAllBlogsAsync());
        }

        [Authorize(Policy = "ApiUser", AuthenticationSchemes = "Bearer")]
        [HttpGet("get-concrete-blog-by-id")]
        public async Task<IActionResult> GetConcreteBlogById(int id)
        {
           return Ok(await _unitOfWork.BaseRepository.GetBlogById(id));
        }

        [Authorize(Policy = "ApiUser", AuthenticationSchemes = "Bearer")]
        [HttpGet("sort-blogs")]
        public async Task<IActionResult> SortBlogs(string sortby, string fitlerbyauthor)
        {
            return Ok(await _repo.SortBlogsAsync(sortby, fitlerbyauthor));
        }
    }
}
